using System.CodeDom;

namespace ScriptGenerator.Editor.Internal
{
    internal interface ITypeFields
    {
        ITypeField PrivateReadOnly(CodeTypeReference type, string name,
            out CodeFieldReferenceExpression fieldReference);

        ITypeField PublicConst(CodeTypeReference type, string name,
            out CodeFieldReferenceExpression fieldReference);
    }
}