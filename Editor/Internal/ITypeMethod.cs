using System.CodeDom;

namespace ScriptGenerator.Editor.Internal
{
    internal interface ITypeMethod
    {
        ITypeMethod Returns(CodeTypeReference type);
        ITypeMethod Void();
        ITypeMethod AddParameter(CodeTypeReference type, string name, out CodeArgumentReferenceExpression argumentReference);
        ITypeMethod Statement(Statements statement);
    }
}