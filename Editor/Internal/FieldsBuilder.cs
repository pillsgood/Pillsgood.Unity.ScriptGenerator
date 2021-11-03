using System.CodeDom;
using UnityEngine;

namespace ScriptGenerator.Editor.Internal
{
    internal class FieldsBuilder : ITypeFields
    {
        private readonly CodeTypeBuilder _codeTypeBuilder;
        private readonly CodeTypeMemberCollection _fields = new();

        public FieldsBuilder(CodeTypeBuilder codeTypeBuilder)
        {
            _codeTypeBuilder = codeTypeBuilder;
        }

        public ITypeField PrivateReadOnly(CodeTypeReference type, string name,
            out CodeFieldReferenceExpression fieldRef)
        {
            var field = new CodeMemberField(type, name)
            {
                Attributes = MemberAttributes.Private
            }.MakeReadonly(out var readonlyField);

            _fields.Add(readonlyField);
            return new FieldBuilder(this, field, out fieldRef);
        }

        public ITypeField Private(CodeTypeReference type, string name,
            out CodeFieldReferenceExpression fieldReference)
        {
            var field = new CodeMemberField(type, name)
            {
                Attributes = MemberAttributes.Private
            };

            _fields.Add(field);
            return new FieldBuilder(this, field, out fieldReference);
        }

        public ITypeField Protected(CodeTypeReference type, string name,
            out CodeFieldReferenceExpression fieldReference)
        {
            var field = new CodeMemberField(type, name)
            {
                Attributes = MemberAttributes.Family
            };

            _fields.Add(field);
            return new FieldBuilder(this, field, out fieldReference);
        }

        public ITypeField Public(CodeTypeReference type, string name,
            out CodeFieldReferenceExpression fieldReference)
        {
            var field = new CodeMemberField(type, name)
            {
                Attributes = MemberAttributes.Public
            };

            _fields.Add(field);
            return new FieldBuilder(this, field, out fieldReference);
        }


        public ITypeField PublicConst(CodeTypeReference type, string name,
            out CodeFieldReferenceExpression fieldReference)
        {
            var field = new CodeMemberField(type, name)
            {
                Attributes = MemberAttributes.Public | MemberAttributes.Const
            };
            _fields.Add(field);
            return new FieldBuilder(this, field, out fieldReference);
        }


        public CodeTypeMemberCollection Result()
        {
            return _fields;
        }

        private class FieldBuilder : ITypeField, ITypeFieldAssignment
        {
            private readonly CodeMemberField _field;
            private readonly CodeFieldReferenceExpression _fieldReference;
            private readonly FieldsBuilder _parent;

            public FieldBuilder(FieldsBuilder parent, CodeMemberField field,
                out CodeFieldReferenceExpression fieldReference)
            {
                _parent = parent;
                _field = field;
                _fieldReference = fieldReference =
                    new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), _field.Name);
            }

            public ITypeFieldAssignment Assign()
            {
                return this;
            }

            public ITypeFieldAssignment EncapsulateGetOnly(string name, MemberAttributes attributes)
            {
                var property = new CodeMemberProperty
                {
                    Attributes = attributes,
                    Type = _field.Type,
                    Name = name,
                    HasGet = true,
                    HasSet = false
                };
                property.GetStatements.Add(new CodeMethodReturnStatement(_fieldReference));
                _parent._fields.Add(property);
                return this;
            }

            public ITypeField InConstructor(CodeExpression assignment)
            {
                var assign = new CodeAssignStatement(_fieldReference, assignment);
                Debug.Assert(_parent._codeTypeBuilder.codeConstructor != null,
                    "_codeTypeBuilder.codeConstructor != null");
                _parent._codeTypeBuilder.codeConstructor.Statements.Add(assign);
                return this;
            }

            public ITypeField Init(CodeExpression expression)
            {
                _field.InitExpression = expression;
                return this;
            }
        }
    }
}