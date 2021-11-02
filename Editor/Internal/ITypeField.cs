using System.CodeDom;

namespace ScriptGenerator.Editor.Internal
{
    internal interface ITypeField
    {
        ITypeFieldAssignment Assign();
        ITypeFieldAssignment EncapsulateGetOnly(string name, MemberAttributes attributes);
    }
}