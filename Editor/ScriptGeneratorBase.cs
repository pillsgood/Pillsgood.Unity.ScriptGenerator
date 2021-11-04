using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.CSharp;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEditor.AssetImporters;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ScriptGenerator.Editor
{
    public abstract class ScriptGeneratorBase<TAsset, TSource> : ScriptGeneratorBase<TSource>
        where TAsset : ScriptGeneratorAsset
        where TSource : Object
    {
        protected sealed override Type AssetType => typeof(TAsset);
    }

    public abstract class ScriptGeneratorBase<TSource> : ScriptedImporter where TSource : Object
    {
        protected const string BaseExtension = "script_generator";
        private static readonly Regex NamespacePattern = new(@"""rootNamespace"": ""(.*)""");

        [SerializeField] private bool autoNamespace;
        [SerializeField] private string codeNamespace;
        [SerializeField] private string codeTypeName;
        [SerializeField] private TSource source;


        protected virtual Type AssetType => typeof(ScriptGeneratorAsset);
        protected TSource SourceObject => source;

        protected CodeCompileUnit TargetUnit { get; private set; }

        protected string Namespace => codeNamespace;

        protected string TypeName => codeTypeName;

        private void OnValidate()
        {
            if (autoNamespace)
            {
                codeNamespace = GetNamespace(Path.GetDirectoryName(assetPath));
            }
        }

        public override void OnImportAsset(AssetImportContext ctx)
        {
            var asset = ScriptableObject.CreateInstance(AssetType);
            asset.name = Path.GetFileNameWithoutExtension(assetPath);
            var assetIcon = EditorIcons.UnityGameObjectIcon;
            ctx.AddObjectToAsset("<root>", asset, assetIcon);
            ctx.SetMainObject(asset);

            if (SourceObject != null)
            {
                ctx.DependsOnSourceAsset(AssetDatabase.GetAssetPath(SourceObject));
                TargetUnit = new CodeCompileUnit();
                var result = GenerateCode();
                if (result)
                {
                    var provider = new CSharpCodeProvider();
                    var options = new CodeGeneratorOptions
                    {
                        BracingStyle = "C",
                    };

                    var dir = Path.GetDirectoryName(assetPath);
                    var fileName = Path.Combine(dir, codeTypeName + ".generated.cs");
                    using var sourceWriter = new StreamWriter(fileName);
                    provider.GenerateCodeFromCompileUnit(TargetUnit, sourceWriter, options);
                    EditorApplication.delayCall += AssetDatabase.Refresh;
                    AssetDatabase.ImportAsset(assetPath);
                    AssetDatabase.Refresh();
                }
            }
        }

        protected static string GetPropertyName(string name)
        {
            var parts = name
                .Split(new[] { ' ', '_' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Length > 1
                    ? char.ToUpperInvariant(s[0]) + s[1..]
                    : s.ToUpperInvariant());

            return string.Join("", parts);
        }

        protected static string GetFieldName(string name)
        {
            var s = GetPropertyName(name);
            return s.Length > 1
                ? char.ToLowerInvariant(s[0]) + s[1..]
                : s.ToLowerInvariant();
        }

        protected static string GetPrivateFieldName(string name)
        {
            return "_" + GetFieldName(name);
        }

        protected static string GetEnumFieldName(string name)
        {
            name = GetPropertyName(name);
            if (char.IsDigit(name[0]))
            {
                if (name.Length == 1)
                {
                    return $"entry_{name}";
                }

                return name[1..].Trim('_') + $"_{name[0]}";
            }

            return name;
        }

        protected abstract bool GenerateCode();

        public static string GetNamespace(string dir)
        {
            var guard = 0;
            var current = dir;
            var parts = new Queue<string>();
            while (guard < 20)
            {
                guard += 1;
                if (string.IsNullOrEmpty(current) || current.EndsWith("Scripts") || current.EndsWith("Assets"))
                {
                    break;
                }

                var asmDef = Directory.EnumerateFiles(current).SingleOrDefault(s => s.EndsWith(".asmdef"));
                if (string.IsNullOrEmpty(asmDef))
                {
                    parts.Enqueue(Path.GetFileNameWithoutExtension(current));
                    current = Path.GetDirectoryName(current);
                    continue;
                }

                var match = NamespacePattern.Match(File.ReadAllText(asmDef));
                if (match.Success)
                {
                    var ns = match.Result("$1");
                    ns += $".{Path.GetFileNameWithoutExtension(current)}";
                    return parts.Aggregate(ns, (current1, part) => current1 + $".{part}");
                }

                break;
            }

            return string.Empty;
        }
    }
}