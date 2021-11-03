using System;
using System.CodeDom;

namespace ScriptGenerator.Editor.Internal.FieldsBuilderExt
{
    internal static class FieldsBuilderProtectedExtension
    {
        public static ITypeField Protected(this ITypeFields source, Type type, string name)
        {
            return source.Protected(new CodeTypeReference(type), name, out _);
        }

        public static ITypeField Protected(this ITypeFields source, Type type, string name,
            out CodeFieldReferenceExpression fieldReference)
        {
            return source.Protected(new CodeTypeReference(type), name, out fieldReference);
        }

        public static ITypeField Protected(this ITypeFields source, string type, string name)
        {
            return source.Protected(new CodeTypeReference(type), name, out _);
        }

        public static ITypeField Protected(this ITypeFields source, string type, string name,
            out CodeFieldReferenceExpression fieldReference)
        {
            return source.Protected(new CodeTypeReference(type), name, out fieldReference);
        }

        public static ITypeField Protected(this ITypeFields source, CodeTypeReference type, string name)
        {
            return source.Protected(type, name, out _);
        }
    }
}