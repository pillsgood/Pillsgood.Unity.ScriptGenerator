using System.CodeDom;

namespace ScriptGenerator.Editor.Internal
{
    internal interface ITypeConstructor
    {
        ITypeConstructor Public();
        ITypeConstructor Protected();

        ITypeConstructor AddParameter(CodeParameterDeclarationExpression expression, out
            CodeArgumentReferenceExpression argumentReference);

        ITypeConstructor Abstract();
        ITypeConstructor Override();
        ITypeConstructor AddBaseParameter(CodeExpression expression);
    }
}