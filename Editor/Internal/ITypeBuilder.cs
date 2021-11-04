using System.CodeDom;

namespace ScriptGenerator.Editor.Internal
{
    internal interface ITypeBuilder
    {
        CodeTypeDeclaration Result();
        CodeTypeReference Reference();
    }
}