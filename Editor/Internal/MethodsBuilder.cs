using System.CodeDom;

namespace ScriptGenerator.Editor.Internal
{
    internal class MethodsBuilder : ITypeMethods
    {
        private readonly CodeTypeBuilder _codeTypeBuilder;
        private readonly CodeTypeMemberCollection _methods = new();

        public MethodsBuilder(CodeTypeBuilder codeTypeBuilder)
        {
            _codeTypeBuilder = codeTypeBuilder;
        }

        public CodeTypeMemberCollection Result()
        {
            return _methods;
        }

        public ITypeMethod Public(string name)
        {
            var method = new CodeMemberMethod()
            {
                Name = name,
                Attributes = MemberAttributes.Public | MemberAttributes.Final
            };
            _methods.Add(method);
            return new TypeMethod(_codeTypeBuilder, method);
        }

        private class TypeMethod : ITypeMethod
        {
            private readonly CodeTypeBuilder _codeTypeBuilder;
            private readonly CodeMemberMethod _method;

            public TypeMethod(CodeTypeBuilder codeTypeBuilder, CodeMemberMethod method)
            {
                _codeTypeBuilder = codeTypeBuilder;
                _method = method;
            }

            public ITypeMethod Returns(CodeTypeReference type)
            {
                _method.ReturnType = type;
                return this;
            }

            public ITypeMethod Void()
            {
                _method.ReturnType = new CodeTypeReference(typeof(void));
                return this;
            }

            public ITypeMethod AddParameter(CodeTypeReference type, string name,
                out CodeArgumentReferenceExpression argumentReference)
            {
                _method.Parameters.Add(new CodeParameterDeclarationExpression(type, name));
                argumentReference = new CodeArgumentReferenceExpression(name);
                return this;
            }

            public ITypeMethod Statement(Statements statement)
            {
                statement?.Invoke(_method.Statements);
                return this;
            }
        }
    }
}