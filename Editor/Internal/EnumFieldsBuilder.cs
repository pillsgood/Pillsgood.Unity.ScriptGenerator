using System.CodeDom;

namespace ScriptGenerator.Editor.Internal
{
    internal class EnumFieldsBuilder : IEnumFields
    {
        private readonly TypeBuilder _typeBuilder;
        private readonly CodeTypeMemberCollection _fields = new();

        public EnumFieldsBuilder(TypeBuilder typeBuilder)
        {
            _typeBuilder = typeBuilder;
        }

        public IEnumFields Add(string name)
        {
            var field = new CodeMemberField(_typeBuilder.typeDeclaration.Name, name);
            _fields.Add(field);
            return this;
        }

        public CodeTypeMemberCollection Result()
        {
            return _fields;
        }
    }
}