using System;
using System.CodeDom;

namespace ScriptGenerator.Editor.Internal.FieldsBuilderExt
{
    internal static class FieldsBuilderPrivateExtensions
    {
        public static ITypeField Private(this ITypeFields source, Type type, string name)
        {
            return source.Private(new CodeTypeReference(type), name, out _);
        }

        public static ITypeField Private(this ITypeFields source, Type type, string name,
            out CodeFieldReferenceExpression fieldReference)
        {
            return source.Private(new CodeTypeReference(type), name, out fieldReference);
        }

        public static ITypeField Private(this ITypeFields source, string type, string name)
        {
            return source.Private(new CodeTypeReference(type), name, out _);
        }

        public static ITypeField Private(this ITypeFields source, string type, string name,
            out CodeFieldReferenceExpression fieldReference)
        {
            return source.Private(new CodeTypeReference(type), name, out fieldReference);
        }

        public static ITypeField Private(this ITypeFields source, CodeTypeReference type, string name)
        {
            return source.Private(type, name, out _);
        }
    }
}