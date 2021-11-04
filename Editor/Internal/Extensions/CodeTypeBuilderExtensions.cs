using System;
using System.CodeDom;

namespace ScriptGenerator.Editor.Internal
{
    internal static class CodeTypeBuilderExtensions
    {
        public static ITypeDeclaration Inherits(this ITypeDeclaration source, Type type)
        {
            return source.Inherits(new CodeTypeReference(type));
        }

        public static ITypeDeclaration Inherits(this ITypeDeclaration source, string type)
        {
            return source.Inherits(new CodeTypeReference(type));
        }

        public static ITypeDeclaration Implements(this ITypeDeclaration source, Type type)
        {
            return source.Implements(new CodeTypeReference(type));
        }

        public static ITypeDeclaration Implements(this ITypeDeclaration source, string type)
        {
            return source.Implements(new CodeTypeReference(type));
        }
    }
}