using System.CodeDom;

namespace ScriptGenerator.Editor.Internal
{
    internal class ConstructorBuilder : ITypeConstructor
    {
        private readonly CodeConstructor _codeConstructor;

        public ConstructorBuilder()
        {
            _codeConstructor = new CodeConstructor
            {
                Attributes = MemberAttributes.Public
            };
        }

        public ITypeConstructor Public()
        {
            _codeConstructor.Attributes =
                (_codeConstructor.Attributes & ~MemberAttributes.AccessMask) | MemberAttributes.Public;
            return this;
        }

        public ITypeConstructor Protected()
        {
            _codeConstructor.Attributes =
                (_codeConstructor.Attributes & ~MemberAttributes.AccessMask) | MemberAttributes.Family;
            return this;
        }

        public ITypeConstructor AddParameter(CodeParameterDeclarationExpression expression,
            out CodeArgumentReferenceExpression argumentReference)
        {
            _codeConstructor.Parameters.Add(expression);
            argumentReference = new CodeArgumentReferenceExpression(expression.Name);
            return this;
        }

        public ITypeConstructor Abstract()
        {
            _codeConstructor.Attributes |= MemberAttributes.Abstract;
            return this;
        }

        public ITypeConstructor Override()
        {
            _codeConstructor.Attributes |= MemberAttributes.Override;
            return this;
        }

        public ITypeConstructor AddBaseParameter(CodeExpression expression)
        {
            _codeConstructor.BaseConstructorArgs.Add(expression);
            return this;
        }

        public CodeConstructor Result()
        {
            return _codeConstructor;
        }
    }
}