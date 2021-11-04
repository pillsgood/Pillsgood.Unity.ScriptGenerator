using System.CodeDom;

namespace ScriptGenerator.Editor.Internal
{
    internal class TypeMethodsBuilder : ITypeMethods
    {
        private readonly TypeBuilder _typeBuilder;
        private readonly CodeTypeMemberCollection _methods = new();

        public TypeMethodsBuilder(TypeBuilder typeBuilder)
        {
            _typeBuilder = typeBuilder;
        }

        public ITypeMethod Public(string name)
        {
            var method = new CodeMemberMethod
            {
                Name = name,
                Attributes = MemberAttributes.Public | MemberAttributes.Final,
            };
            _methods.Add(method);
            return new TypeMethod(_typeBuilder, method);
        }

        public CodeTypeMemberCollection Result()
        {
            return _methods;
        }

        private class TypeMethod : ITypeMethod
        {
            private readonly TypeBuilder _typeBuilder;
            private readonly CodeMemberMethod _method;

            public TypeMethod(TypeBuilder typeBuilder, CodeMemberMethod method)
            {
                _typeBuilder = typeBuilder;
                _method = method;
            }

            public ITypeMethod Return(CodeTypeReference type)
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