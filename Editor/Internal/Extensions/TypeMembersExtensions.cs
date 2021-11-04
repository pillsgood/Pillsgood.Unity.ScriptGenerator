using System;

namespace ScriptGenerator.Editor.Internal
{
    internal static class TypeMembersExtensions
    {
        public static ITypeMembers AddNestedType(this ITypeMembers source, Action<ITypeDeclaration> build)
        {
            return source.AddNestedType(build, out _);
        }
    }
}