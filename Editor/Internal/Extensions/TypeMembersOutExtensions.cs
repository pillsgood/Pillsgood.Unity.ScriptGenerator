using System;

namespace ScriptGenerator.Editor.Internal
{
    internal static class TypeMembersOutExtensions
    {
        public static ITypeMembers Fields<T>(this ITypeMembers source, out T arg, Action<ITypeFields> build)
        {
            arg = default;
            return source.Fields(build);
        }

        public static ITypeMembers Fields<T1, T2>(this ITypeMembers source, out T1 arg1, out T2 arg2,
            Action<ITypeFields> build)
        {
            arg1 = default;
            arg2 = default;
            return source.Fields(build);
        }

        public static ITypeMembers Fields<T1, T2, T3>(this ITypeMembers source, out T1 arg1, out T2 arg2, out T3 arg3,
            Action<ITypeFields> build)
        {
            arg1 = default;
            arg2 = default;
            arg3 = default;
            return source.Fields(build);
        }

        public static ITypeMembers Constructor<T>(this ITypeMembers source, out T arg, Action<ITypeConstructor> build)
        {
            arg = default;
            return source.Constructor(build);
        }

        public static ITypeMembers Constructor<T1, T2>(this ITypeMembers source, out T1 arg1, out T2 arg2,
            Action<ITypeConstructor> build)
        {
            arg1 = default;
            arg2 = default;
            return source.Constructor(build);
        }

        public static ITypeMembers Constructor<T1, T2, T3>(this ITypeMembers source, out T1 arg1, out T2 arg2,
            out T3 arg3,
            Action<ITypeConstructor> build)
        {
            arg1 = default;
            arg2 = default;
            arg3 = default;
            return source.Constructor(build);
        }
    }
}