using System;
using System.CodeDom;

namespace ScriptGenerator.Editor.Internal
{
    // internal delegate void FieldsOut<T>(ITypeFields fields, out T field);

    internal interface ICodeTypeMembers : ICodeTypeBuilder
    {
        ICodeTypeMembers AddNestedType(Action<ICodeTypeDeclaration> build);
        ICodeTypeMembers AddNestedType(Action<ICodeTypeDeclaration> build, out CodeTypeReference typeDeclaration);
        ICodeTypeMembers Constructor(Action<ITypeConstructor> build);
        ICodeTypeMembers Fields(Action<ITypeFields> build);
        ICodeTypeMembers Fields<T>(out T arg, Action<ITypeFields> build);
        ICodeTypeMembers Fields<T1, T2>(out T1 arg1, out T2 arg2, Action<ITypeFields> build);
        ICodeTypeMembers Properties(Action<ITypeProperties> action);
    }
}