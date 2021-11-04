using System;
using System.CodeDom;

namespace ScriptGenerator.Editor.Internal
{
    internal interface ITypeMembers : ITypeBuilder
    {
        ITypeMembers AddNestedType(Action<ITypeDeclaration> build, out CodeTypeReference typeDeclaration);
        ITypeMembers Constructor(Action<ITypeConstructor> build);
        ITypeMembers Fields(Action<ITypeFields> build);
        ITypeMembers Properties(Action<ITypeProperties> build);
        ITypeMembers EnumFields(Action<IEnumFields> build);
        ITypeMembers Methods(Action<ITypeMethods> build);
    }
}