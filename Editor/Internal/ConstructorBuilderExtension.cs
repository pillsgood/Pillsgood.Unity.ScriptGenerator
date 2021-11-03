using System;
using System.CodeDom;

namespace ScriptGenerator.Editor.Internal
{
    internal static class TypeConstructorExtension
    {
        public static ITypeConstructor AddParameter(this ITypeConstructor constructor,
            CodeParameterDeclarationExpression parameter)
        {
            return constructor.AddParameter(parameter, out _);
        }

        public static ITypeConstructor AddParameter(this ITypeConstructor source, Type type, string name)
        {
            return source.AddParameter(new CodeParameterDeclarationExpression(type, name));
        }

        public static ITypeConstructor AddParameter(this ITypeConstructor source, Type type, string name,
            out CodeArgumentReferenceExpression argumentReference)
        {
            var parameter = new CodeParameterDeclarationExpression(type, name);
            return source.AddParameter(parameter, out argumentReference);
        }
    }
}