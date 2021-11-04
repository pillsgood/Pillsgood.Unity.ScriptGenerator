using System;
using System.CodeDom;

namespace ScriptGenerator.Editor.Internal
{
    internal interface ITypeMembers : ITypeBuilder
    {
        ITypeMembers AddNestedType(Action<ITypeDeclaration> build);
        ITypeMembers AddNestedType(Action<ITypeDeclaration> build, out CodeTypeReference typeDeclaration);
        ITypeMembers Constructor(Action<ITypeConstructor> build);
        ITypeMembers Constructor<T>(out T arg, Action<ITypeConstructor> build);
        ITypeMembers Fields(Action<ITypeFields> build);
        ITypeMembers Fields<T>(out T arg, Action<ITypeFields> build);
        ITypeMembers Fields<T1, T2>(out T1 arg1, out T2 arg2, Action<ITypeFields> build);
        ITypeMembers Properties(Action<ITypeProperties> build);
        ITypeMembers EnumFields(Action<IEnumFields> build);
        ITypeMembers Methods(Action<ITypeMethods> build);
    }
}