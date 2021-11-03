using System;
using System.CodeDom;

namespace ScriptGenerator.Editor.Internal.PropertiesBuilderExt
{
    internal static class PropertiesBuilderProtectedExtension
    {
        public static ITypeProperty Protected(this ITypeProperties source, Type type, string name)
        {
            return source.Protected(new CodeTypeReference(type), name, out _);
        }

        public static ITypeProperty Protected(this ITypeProperties source, Type type, string name,
            out CodePropertyReferenceExpression propertyReference)
        {
            return source.Protected(new CodeTypeReference(type), name, out propertyReference);
        }

        public static ITypeProperty Protected(this ITypeProperties source, string type, string name)
        {
            return source.Protected(new CodeTypeReference(type), name, out _);
        }

        public static ITypeProperty Protected(this ITypeProperties source, string type, string name,
            out CodePropertyReferenceExpression propertyReference)
        {
            return source.Protected(new CodeTypeReference(type), name, out propertyReference);
        }

        public static ITypeProperty Protected(this ITypeProperties source, CodeTypeDeclaration type, string name)
        {
            return source.Protected(new CodeTypeReference(type.Name), name, out _);
        }

        public static ITypeProperty Protected(this ITypeProperties source, CodeTypeDeclaration type, string name,
            out CodePropertyReferenceExpression propertyReference)
        {
            return source.Protected(new CodeTypeReference(type.Name), name, out propertyReference);
        }
    }
}