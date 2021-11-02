using System;
using System.CodeDom;

namespace ScriptGenerator.Editor.Internal
{
    internal static class CodeTypeBuilderExtension
    {
        public static ICodeTypeDeclaration Inherits(this ICodeTypeDeclaration source, Type type)
        {
            return source.Inherits(new CodeTypeReference(type));
        }

        public static ICodeTypeDeclaration Inherits(this ICodeTypeDeclaration source, string type)
        {
            return source.Inherits(new CodeTypeReference(type));
        }

        public static ICodeTypeDeclaration Implements(this ICodeTypeDeclaration source, Type type)
        {
            return source.Implements(new CodeTypeReference(type));
        }

        public static ICodeTypeDeclaration Implements(this ICodeTypeDeclaration source, string type)
        {
            return source.Implements(new CodeTypeReference(type));
        }
    }
}