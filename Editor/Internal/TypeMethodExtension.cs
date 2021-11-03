using System;
using System.CodeDom;

namespace ScriptGenerator.Editor.Internal
{
    internal static class TypeMethodExtension
    {
        public static ITypeMethod AddParameter(this ITypeMethod source, Type type, string name,
            out CodeArgumentReferenceExpression argumentReference)
        {
            return source.AddParameter(new CodeTypeReference(type), name, out argumentReference);
        }

        public static ITypeMethod AddParameter(this ITypeMethod source, string type, string name,
            out CodeArgumentReferenceExpression argumentReference)
        {
            return source.AddParameter(new CodeTypeReference(type), name, out argumentReference);
        }

        public static ITypeMethod AddParameter(this ITypeMethod source, Type type, string name)
        {
            return source.AddParameter(type, name, out _);
        }

        public static ITypeMethod AddParameter(this ITypeMethod source, string type, string name)
        {
            return source.AddParameter(type, name, out _);
        }

        public static ITypeMethod Return(this ITypeMethod source, Type type)
        {
            return source.Returns(new CodeTypeReference(type));
        }

        public static ITypeMethod Return(this ITypeMethod source, string type)
        {
            return source.Returns(new CodeTypeReference(type));
        }
    }
}