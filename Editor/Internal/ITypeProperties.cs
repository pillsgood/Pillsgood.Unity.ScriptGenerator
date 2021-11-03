using System.CodeDom;

namespace ScriptGenerator.Editor.Internal
{
    internal interface ITypeProperties
    {
        ITypeProperty Public(CodeTypeReference type, string name,
            out CodePropertyReferenceExpression propertyReference);

        ITypeProperty Protected(CodeTypeReference type, string name,
            out CodePropertyReferenceExpression propertyReference);

        ITypeProperty Private(CodeTypeReference type, string name,
            out CodePropertyReferenceExpression propertyReference);
    }
}