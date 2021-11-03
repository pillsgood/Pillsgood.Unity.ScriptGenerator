using System.CodeDom;

namespace ScriptGenerator.Editor.Internal
{
    internal interface ITypeFields
    {
        ITypeField PrivateReadOnly(CodeTypeReference type, string name,
            out CodeFieldReferenceExpression fieldReference);

        ITypeField Private(CodeTypeReference type, string name,
            out CodeFieldReferenceExpression fieldReference);

        ITypeField Protected(CodeTypeReference type, string name,
            out CodeFieldReferenceExpression fieldReference);

        ITypeField Public(CodeTypeReference type, string name,
            out CodeFieldReferenceExpression fieldReference);


        ITypeField PublicConst(CodeTypeReference type, string name,
            out CodeFieldReferenceExpression fieldReference);
    }
}