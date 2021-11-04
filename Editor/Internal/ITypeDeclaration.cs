using System.CodeDom;
using System.Reflection;

namespace ScriptGenerator.Editor.Internal
{
    internal interface ITypeDeclaration : ITypeBuilder
    {
        ITypeDeclaration Name(string name);
        ITypeDeclaration IsClass();
        ITypeDeclaration TypeAttributes(TypeAttributes typeAttributes);
        ITypeDeclaration IsPartial();
        ITypeDeclaration Inherits(CodeTypeReference type);
        ITypeDeclaration Implements(CodeTypeReference type);
        ITypeDeclaration IsEnum();
        ITypeMembers Members();
    }
}