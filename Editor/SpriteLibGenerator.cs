#if U2D_ANIMATION
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ScriptGenerator.Editor.Internal;
using ScriptGenerator.Editor.Internal.FieldsBuilderExt;
using ScriptGenerator.Runtime.SpriteLibraryBindings;
using UnityEditor;
using UnityEditor.AssetImporters;
using UnityEngine;
using UnityEngine.U2D.Animation;

namespace ScriptGenerator.Editor
{
    [ScriptedImporter(1, Extension)]
    public class SpriteLibGenerator : ScriptGeneratorBase<SpriteLibraryAsset>
    {
        private const string Extension = "spritelib_" + BaseExtension;
        private Dictionary<string, CodeTypeReference> _categoryTypes;

        [MenuItem("Assets/Create/Generators/Sprite Library Bindings")]
        public static void CreateAsset()
        {
            ProjectWindowUtil.CreateAssetWithContent($"SpriteLibraryGenerator.{Extension}", "{}");
        }

        protected override bool GenerateCode()
        {
            TargetUnit.WithNamespace()
                .AddImport(new CodeNamespaceImport(typeof(Sprite).Namespace))
                .AddImport(new CodeNamespaceImport(typeof(SpriteLibraryAsset).Namespace))
                .AddImport(new CodeNamespaceImport(typeof(SpriteLibCategory).Namespace));

            TargetUnit.WithNamespace(Namespace)
                .AddType(BuildTargetClass);

            return true;
        }

        private void BuildTargetClass(ITypeDeclaration builder)
        {
            builder.Name($"{TypeName}")
                .IsClass()
                .IsPartial()
                .TypeAttributes(TypeAttributes.Public)
                .Members()
                .AddNestedTypes(BuildCategoryTypes)
                .AddNestedType(BuildSpriteLibCategories, out var categoriesTypeReference)
                .Constructor(out CodeArgumentReferenceExpression spriteLibParamRef,
                    constructor =>
                    {
                        constructor.Public()
                            .AddParameter(typeof(SpriteLibraryAsset), "libraryAsset", out spriteLibParamRef);
                    })
                .Fields(fields =>
                {
                    fields.PrivateReadonly(categoriesTypeReference, "_categories")
                        .Assign()
                        .InConstructor(new CodeObjectCreateExpression(categoriesTypeReference, spriteLibParamRef))
                        .EncapsulateGetOnly("Categories", MemberAttributes.Public | MemberAttributes.Final);
                });
        }

        private IEnumerable<Action<ITypeDeclaration>> BuildCategoryTypes()
        {
            _categoryTypes = new Dictionary<string, CodeTypeReference>();
            return SourceObject.GetCategoryNames().Select(categoryName => (Action<ITypeDeclaration>) (builder =>
            {
                _categoryTypes[categoryName] = builder.Name($"{GetPropertyName(categoryName)}_Category")
                    .IsClass()
                    .Inherits(typeof(SpriteLibCategory))
                    .Members()
                    .AddNestedType(enumDeclaration =>
                    {
                        enumDeclaration.Name("Label")
                            .TypeAttributes(TypeAttributes.Public)
                            .IsEnum()
                            .Members()
                            .EnumFields(fields =>
                            {
                                foreach (var labelName in SourceObject.GetCategoryLabelNames(categoryName))
                                {
                                    fields.Add(GetEnumFieldName(labelName));
                                }
                            });
                    }, out var enumTypeRef)
                    .Constructor(constructor =>
                    {
                        constructor.Public()
                            .AddParameter(typeof(string), "categoryName", out var nameParamRef)
                            .AddParameter(typeof(SpriteLibraryAsset), "libraryAsset", out var spriteLibParamRef)
                            .AddBaseParameter(nameParamRef)
                            .AddBaseParameter(spriteLibParamRef);
                    })
                    .Methods(methods =>
                    {
                        methods.Public("Get")
                            .Return(typeof(Sprite))
                            .AddParameter(enumTypeRef, "label", out var paramRef)
                            .Statement(statements =>
                            {
                                var index = new CodeCastExpression(typeof(int), paramRef);
                                var indexer = new CodeIndexerExpression(new CodeThisReferenceExpression(), index);
                                statements.Add(new CodeMethodReturnStatement(indexer));
                            });
                    })
                    .Reference();
            }));
        }

        private void BuildSpriteLibCategories(ITypeDeclaration builder)
        {
            builder.Name("SpriteLibCategories")
                .IsClass()
                .TypeAttributes(TypeAttributes.NestedPublic)
                .Members()
                .Constructor(out CodeArgumentReferenceExpression spriteLibArgRef, constructor =>
                {
                    constructor.Public()
                        .AddParameter(typeof(SpriteLibraryAsset), "libraryAsset", out spriteLibArgRef);
                })
                .Fields(fields =>
                {
                    foreach (var (categoryName, typeRef) in _categoryTypes)
                    {
                        var create = new CodeObjectCreateExpression(typeRef,
                            new CodePrimitiveExpression(categoryName), spriteLibArgRef);

                        fields.PrivateReadonly(typeRef, GetPrivateFieldName(categoryName))
                            .Assign()
                            .InConstructor(create)
                            .EncapsulateGetOnly(GetPropertyName(categoryName),
                                MemberAttributes.Public | MemberAttributes.Final);
                    }
                });
        }
    }
}
#endif