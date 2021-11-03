using System;
using System.CodeDom;

namespace ScriptGenerator.Editor.Internal.PropertiesBuilderExt
{
    internal static class PropertiesBuilderPrivateExtension
    {
        public static ITypeProperty Private(this ITypeProperties source, Type type, string name)
        {
            return source.Private(new CodeTypeReference(type), name, out _);
        }

        public static ITypeProperty Private(this ITypeProperties source, Type type, string name,
            out CodePropertyReferenceExpression propertyReference)
        {
            return source.Private(new CodeTypeReference(type), name, out propertyReference);
        }

        public static ITypeProperty Private(this ITypeProperties source, string type, string name)
        {
            return source.Private(new CodeTypeReference(type), name, out _);
        }

        public static ITypeProperty Private(this ITypeProperties source, string type, string name,
            out CodePropertyReferenceExpression propertyReference)
        {
            return source.Private(new CodeTypeReference(type), name, out propertyReference);
        }

        public static ITypeProperty Private(this ITypeProperties source, CodeTypeDeclaration type, string name)
        {
            return source.Private(new CodeTypeReference(type.Name), name, out _);
        }

        public static ITypeProperty Private(this ITypeProperties source, CodeTypeDeclaration type, string name,
            out CodePropertyReferenceExpression propertyReference)
        {
            return source.Private(new CodeTypeReference(type.Name), name, out propertyReference);
        }
    }
}