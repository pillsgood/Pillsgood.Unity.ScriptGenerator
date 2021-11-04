using System;
using System.CodeDom;

namespace ScriptGenerator.Editor.Internal.FieldsBuilderExt
{
    internal static class FieldsBuilderPublicConstExtensions
    {
        public static ITypeField PublicConst(this ITypeFields source, Type type, string name)
        {
            return source.PublicConst(new CodeTypeReference(type), name, out _);
        }

        public static ITypeField PublicConst(this ITypeFields source, Type type, string name,
            out CodeFieldReferenceExpression fieldReference)
        {
            return source.PublicConst(new CodeTypeReference(type), name, out fieldReference);
        }

        public static ITypeField PublicConst(this ITypeFields source, string type, string name)
        {
            return source.PublicConst(new CodeTypeReference(type), name, out _);
        }

        public static ITypeField PublicConst(this ITypeFields source, string type, string name,
            out CodeFieldReferenceExpression fieldReference)
        {
            return source.PublicConst(new CodeTypeReference(type), name, out fieldReference);
        }

        public static ITypeField PublicConst(this ITypeFields source, CodeTypeReference type, string name)
        {
            return source.PublicConst(type, name, out _);
        }
    }
}