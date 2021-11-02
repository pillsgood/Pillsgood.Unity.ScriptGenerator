using System.CodeDom;
using System.Reflection;

namespace ScriptGenerator.Editor.Internal
{
    internal interface ICodeTypeDeclaration : ICodeTypeBuilder
    {
        ICodeTypeDeclaration Name(string name);
        ICodeTypeDeclaration IsClass();
        ICodeTypeDeclaration TypeAttributes(TypeAttributes typeAttributes);
        ICodeTypeDeclaration IsPartial();
        ICodeTypeDeclaration Inherits(CodeTypeReference type);
        ICodeTypeDeclaration Implements(CodeTypeReference type);
        ICodeTypeMembers Members();
    }
}