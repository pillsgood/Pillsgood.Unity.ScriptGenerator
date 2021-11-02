using System;
using System.CodeDom;

namespace ScriptGenerator.Editor.Internal
{
    internal class CodeNamespaceBuilder
    {
        private readonly CodeNamespace _codeNamespace;

        public CodeNamespaceBuilder(CodeNamespace codeNamespace)
        {
            _codeNamespace = codeNamespace;
        }

        public CodeNamespaceBuilder AddImport(CodeNamespaceImport codeImport)
        {
            _codeNamespace.Imports.Add(codeImport);
            return this;
        }

        public CodeNamespaceBuilder AddType(Action<ICodeTypeDeclaration> build)
        {
            return AddType(build, out _);
        }

        public CodeNamespaceBuilder AddType(Action<ICodeTypeDeclaration> build, out CodeTypeReference typeReference)
        {
            var builder = new CodeTypeBuilder();
            build?.Invoke(builder);
            var typeDeclaration = builder.Result();
            _codeNamespace.Types.Add(typeDeclaration);
            typeReference = new CodeTypeReference(typeDeclaration.Name);
            return this;
        }

        public CodeNamespace Result()
        {
            return _codeNamespace;
        }
    }
}