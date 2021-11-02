using System;
using System.CodeDom;

namespace ScriptGenerator.Editor.Internal.FieldsBuilderExt
{
    internal static class FieldsBuilderPrivateReadonlyExtension
    {
        public static ITypeField PrivateReadonly(this ITypeFields source, Type type, string name)
        {
            return source.PrivateReadOnly(new CodeTypeReference(type), name, out _);
        }

        public static ITypeField PrivateReadonly(this ITypeFields source, Type type, string name,
            out CodeFieldReferenceExpression fieldReference)
        {
            return source.PrivateReadOnly(new CodeTypeReference(type), name, out fieldReference);
        }

        public static ITypeField PrivateReadonly(this ITypeFields source, string type, string name)
        {
            return source.PrivateReadOnly(new CodeTypeReference(type), name, out _);
        }

        public static ITypeField PrivateReadonly(this ITypeFields source, string type, string name,
            out CodeFieldReferenceExpression fieldReference)
        {
            return source.PrivateReadOnly(new CodeTypeReference(type), name, out fieldReference);
        }

        public static ITypeField PrivateReadonly(this ITypeFields source, CodeTypeReference type, string name)
        {
            return source.PrivateReadOnly(type, name, out _);
        }
    }
}