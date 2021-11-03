using System.CodeDom;

namespace ScriptGenerator.Editor.Internal
{
    internal class EnumFieldsBuilder : IEnumFields
    {
        private readonly CodeTypeBuilder _codeTypeBuilder;
        private readonly CodeTypeMemberCollection _fields = new();

        public EnumFieldsBuilder(CodeTypeBuilder codeTypeBuilder)
        {
            _codeTypeBuilder = codeTypeBuilder;
        }

        public CodeTypeMemberCollection Result()
        {
            return _fields;
        }

        public IEnumFields Add(string name)
        {
            var field = new CodeMemberField(_codeTypeBuilder.typeDeclaration.Name, name);
            _fields.Add(field);
            return this;
        }
    }
}