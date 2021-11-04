using System;
using System.CodeDom;

namespace ScriptGenerator.Editor.Internal
{
    internal class NamespaceBuilder
    {
        private readonly CodeNamespace _codeNamespace;

        public NamespaceBuilder(CodeNamespace codeNamespace)
        {
            _codeNamespace = codeNamespace;
        }

        public NamespaceBuilder AddImport(CodeNamespaceImport codeImport)
        {
            _codeNamespace.Imports.Add(codeImport);
            return this;
        }

        public NamespaceBuilder AddType(Action<ITypeDeclaration> build)
        {
            return AddType(build, out _);
        }

        public NamespaceBuilder AddType(Action<ITypeDeclaration> build, out CodeTypeReference typeReference)
        {
            var builder = new TypeBuilder();
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