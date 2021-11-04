using System;
using System.CodeDom;

namespace ScriptGenerator.Editor.Internal.FieldsBuilderExt
{
    internal static class FieldsBuilderPublicExtensions
    {
        public static ITypeField Public(this ITypeFields source, Type type, string name)
        {
            return source.Public(new CodeTypeReference(type), name, out _);
        }

        public static ITypeField Public(this ITypeFields source, Type type, string name,
            out CodeFieldReferenceExpression fieldReference)
        {
            return source.Public(new CodeTypeReference(type), name, out fieldReference);
        }

        public static ITypeField Public(this ITypeFields source, string type, string name)
        {
            return source.Public(new CodeTypeReference(type), name, out _);
        }

        public static ITypeField Public(this ITypeFields source, string type, string name,
            out CodeFieldReferenceExpression fieldReference)
        {
            return source.Public(new CodeTypeReference(type), name, out fieldReference);
        }

        public static ITypeField Public(this ITypeFields source, CodeTypeReference type, string name)
        {
            return source.Public(type, name, out _);
        }
    }
}