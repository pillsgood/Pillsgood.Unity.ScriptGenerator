using System;
using System.CodeDom;

namespace ScriptGenerator.Editor.Internal.PropertiesBuilderExt
{
    internal static class PropertiesBuilderPublicExtension
    {
        public static ITypeProperty Public(this ITypeProperties source, Type type, string name)
        {
            return source.Public(new CodeTypeReference(type), name, out _);
        }

        public static ITypeProperty Public(this ITypeProperties source, Type type, string name,
            out CodePropertyReferenceExpression propertyReference)
        {
            return source.Public(new CodeTypeReference(type), name, out propertyReference);
        }

        public static ITypeProperty Public(this ITypeProperties source, string type, string name)
        {
            return source.Public(new CodeTypeReference(type), name, out _);
        }

        public static ITypeProperty Public(this ITypeProperties source, string type, string name,
            out CodePropertyReferenceExpression propertyReference)
        {
            return source.Public(new CodeTypeReference(type), name, out propertyReference);
        }

        public static ITypeProperty Public(this ITypeProperties source, CodeTypeDeclaration type, string name)
        {
            return source.Public(new CodeTypeReference(type.Name), name, out _);
        }

        public static ITypeProperty Public(this ITypeProperties source, CodeTypeDeclaration type, string name,
            out CodePropertyReferenceExpression propertyReference)
        {
            return source.Public(new CodeTypeReference(type.Name), name, out propertyReference);
        }
    }
}