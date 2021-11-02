using System.CodeDom;

namespace ScriptGenerator.Editor.Internal
{
    internal interface ITypeFieldAssignment
    {
        ITypeField InConstructor(CodeExpression assignment);
        ITypeField Init(CodeExpression expression);
    }
}