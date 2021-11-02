using System.CodeDom;

namespace ScriptGenerator.Editor.Internal
{
    internal interface ICodeTypeBuilder
    {
        CodeTypeDeclaration Result();
    }
}