using System.CodeDom;

namespace ScriptGenerator.Editor.Internal
{
    internal interface ITypeConstructor
    {
        ITypeConstructor Public();
        ITypeConstructor Protected();
        ITypeConstructor AddParameter(CodeParameterDeclarationExpression expression);
        ITypeConstructor Abstract();
        ITypeConstructor Override();
        ITypeConstructor AddBaseParameter(CodeExpression expression);
    }
}