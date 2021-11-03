using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ScriptGenerator.Editor.Internal;
using ScriptGenerator.Editor.Internal.FieldsBuilderExt;
using ScriptGenerator.Editor.Internal.PropertiesBuilderExt;
using ScriptGenerator.Runtime.AnimatorBindings;
using UnityEditor;
using UnityEditor.Animations;
using UnityEditor.AssetImporters;
using UnityEngine;

namespace ScriptGenerator.Editor
{
    [ScriptedImporter(1, Extension)]
    public class AnimatorBindingGenerator : ScriptGeneratorBase<AnimatorController>
    {
        private const string Extension = BaseExtension + "animator_controller";
        private Dictionary<string, CodeFieldReferenceExpression> _paramRefs;

        [MenuItem("Assets/Create/Generators/Animator Bindings")]
        public static void CreateAsset()
        {
            ProjectWindowUtil.CreateAssetWithContent($"AnimatorBindingGenerator.{Extension}", "{}");
        }

        protected override bool GenerateCode()
        {
            TargetUnit.WithNamespace()
                .AddImport(new CodeNamespaceImport(nameof(System)))
                .AddImport(new CodeNamespaceImport(nameof(UnityEngine)));

            TargetUnit.WithNamespace(Namespace)
                .AddType(BuildTargetBaseClass, out var baseTypeReference)
                .AddType(declaration =>
                {
                    declaration.Name(TypeName)
                        .IsClass()
                        .TypeAttributes(TypeAttributes.Public)
                        .IsPartial()
                        .Inherits(baseTypeReference)
                        .Members()
                        .Constructor(constructor =>
                        {
                            var animatorArg = new CodeParameterDeclarationExpression(typeof(Animator), "animator");
                            constructor.Public().Override()
                                .AddParameter(animatorArg)
                                .AddBaseParameter(new CodeArgumentReferenceExpression(animatorArg.Name));
                        });
                });


            return true;
        }

        private void BuildTargetBaseClass(ICodeTypeDeclaration builder)
        {
            builder.Name($"{TypeName}_Bindings")
                .IsClass()
                .TypeAttributes(TypeAttributes.Public | TypeAttributes.Abstract)
                .Members()
                .AddNestedType(BuildAnimationLayers, out var animLayersType)
                .AddNestedType(BuildAnimationParameters)
                .Constructor(out CodeArgumentReferenceExpression animatorParamRef, constructor =>
                {
                    constructor.Abstract().Protected()
                        .AddParameter(typeof(Animator), "animator", out animatorParamRef);
                })
                .Fields(out CodeFieldReferenceExpression animatorRef, fields =>
                {
                    fields.PrivateReadonly(typeof(Animator), "_animator", out animatorRef)
                        .Assign()
                        .InConstructor(animatorParamRef)
                        .EncapsulateGetOnly("Animator", MemberAttributes.Public);

                    fields.PrivateReadonly(animLayersType, "_layers")
                        .Assign()
                        .InConstructor(new CodeObjectCreateExpression(animLayersType, animatorParamRef))
                        .EncapsulateGetOnly("Layers", MemberAttributes.Public);

                    foreach (var parameter in SourceObject.parameters.Where(param =>
                        param.type is AnimatorControllerParameterType.Trigger))
                        fields.PrivateReadonly(typeof(AnimationTrigger), $"{GetPrivateFieldName(parameter.name)}")
                            .Assign()
                            .InConstructor(new CodeObjectCreateExpression(typeof(AnimationTrigger),
                                animatorRef, _paramRefs[parameter.name]))
                            .EncapsulateGetOnly(GetPropertyName(parameter.name), MemberAttributes.Public);
                })
                .Properties(properties =>
                {
                    foreach (var parameter in SourceObject.parameters.Where(param =>
                        param.type is not AnimatorControllerParameterType.Trigger))
                    {
                        var (type, getMethod, setMethod) = parameter.type switch
                        {
                            AnimatorControllerParameterType.Float =>
                                (typeof(float), nameof(Animator.GetFloat), nameof(Animator.SetFloat)),
                            AnimatorControllerParameterType.Int =>
                                (typeof(int), nameof(Animator.GetInteger), nameof(Animator.SetInteger)),
                            AnimatorControllerParameterType.Bool =>
                                (typeof(bool), nameof(Animator.GetBool), nameof(Animator.SetBool)),
                            _ => throw new ArgumentOutOfRangeException()
                        };
                        properties.Public(type, GetPropertyName(parameter.name))
                            .Virtual()
                            .Get(statements =>
                            {
                                var getMethodReference =
                                    new CodeMethodReferenceExpression(animatorRef, getMethod);
                                var getMethodInvoke =
                                    new CodeMethodInvokeExpression(getMethodReference, _paramRefs[parameter.name]);

                                statements.Add(new CodeMethodReturnStatement(getMethodInvoke));
                            })
                            .Set(statements =>
                            {
                                var setMethodReference = new CodeMethodReferenceExpression(animatorRef, setMethod);
                                var setMethodInvoke = new CodeMethodInvokeExpression(setMethodReference,
                                    _paramRefs[parameter.name],
                                    new CodePropertySetValueReferenceExpression());
                                statements.Add(setMethodInvoke);
                            });
                    }
                });
        }

        private void BuildAnimationParameters(ICodeTypeDeclaration builder)
        {
            var codeTypeDeclaration = builder.Name("AnimationParameters")
                .IsClass()
                .TypeAttributes(TypeAttributes.NestedPrivate)
                .Members()
                .Fields(out CodeFieldReferenceExpression[] fieldRefs, fields =>
                {
                    fieldRefs = new CodeFieldReferenceExpression[SourceObject.parameters.Length];
                    for (var index = 0; index < SourceObject.parameters.Length; index++)
                    {
                        var parameter = SourceObject.parameters[index];
                        fields.PublicConst(typeof(int), GetFieldName(parameter.name), out var fieldRef)
                            .Assign()
                            .Init(new CodePrimitiveExpression(parameter.nameHash));
                        fieldRefs[index] = fieldRef;
                    }
                })
                .Result();

            _paramRefs = new Dictionary<string, CodeFieldReferenceExpression>();
            for (var i = 0; i < fieldRefs.Length; i++)
            {
                var paramName = SourceObject.parameters[i].name;
                var reference = new CodeFieldReferenceExpression(
                    new CodeTypeReferenceExpression(codeTypeDeclaration.Name),
                    fieldRefs[i].FieldName);
                _paramRefs[paramName] = reference;
            }
        }

        private void BuildAnimationLayers(ICodeTypeDeclaration builder)
        {
            // constructor
            var animatorArg = new CodeParameterDeclarationExpression(typeof(Animator), "animator");

            builder.Name("AnimLayers")
                .IsClass()
                .TypeAttributes(TypeAttributes.NestedPublic)
                .Members()
                .Constructor(constructor =>
                {
                    constructor.Public()
                        .AddParameter(animatorArg);
                })
                .Fields(fields =>
                {
                    fields.PrivateReadonly(typeof(Animator), "_animator", out var animatorRef)
                        .Assign()
                        .InConstructor(new CodeArgumentReferenceExpression(animatorArg.Name));

                    for (var index = 0; index < SourceObject.layers.Length; index++)
                    {
                        var layer = SourceObject.layers[index];
                        var propertyName = GetPropertyName(layer.name)
                            .Replace("Layer", string.Empty, StringComparison.OrdinalIgnoreCase);

                        fields.PrivateReadonly(typeof(AnimatorLayer), GetPrivateFieldName(layer.name))
                            .Assign()
                            .InConstructor(new CodeObjectCreateExpression(typeof(AnimatorLayer),
                                animatorRef, new CodePrimitiveExpression(index)))
                            .EncapsulateGetOnly(propertyName, MemberAttributes.Public | MemberAttributes.Final);
                    }
                });
        }
    }
}