using System;
using System.Collections.Generic;

namespace ScriptGenerator.Editor.Internal
{
    internal static class TypeMembersExtensions
    {
        public static ITypeMembers AddNestedType(this ITypeMembers source, Action<ITypeDeclaration> build)
        {
            return source.AddNestedType(build, out _);
        }

        public static ITypeMembers AddNestedTypes(this ITypeMembers source,
            Func<IEnumerable<Action<ITypeDeclaration>>> build)
        {
            foreach (var action in build.Invoke())
            {
                source.AddNestedType(action, out _);
            }

            return source;
        }
    }
}