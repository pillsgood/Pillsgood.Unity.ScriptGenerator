using System.CodeDom;

namespace ScriptGenerator.Editor.Internal
{
    internal class PropertiesBuilder : ITypeProperties
    {
        private readonly CodeTypeMemberCollection _properties = new();
        private readonly CodeTypeBuilder _typeBuilder;

        public PropertiesBuilder(CodeTypeBuilder typeBuilder)
        {
            _typeBuilder = typeBuilder;
        }

        public ITypeProperty Public(CodeTypeReference type, string name,
            out CodePropertyReferenceExpression propertyReference)
        {
            var property = new CodeMemberProperty
            {
                Type = type,
                Name = name,
                Attributes = MemberAttributes.Public | MemberAttributes.Final
            };
            _properties.Add(property);
            return new PropertyBuilder(this, property, out propertyReference);
        }

        public ITypeProperty Protected(CodeTypeReference type, string name,
            out CodePropertyReferenceExpression propertyReference)
        {
            var property = new CodeMemberProperty
            {
                Type = type,
                Name = name,
                Attributes = MemberAttributes.Family | MemberAttributes.Final
            };
            _properties.Add(property);
            return new PropertyBuilder(this, property, out propertyReference);
        }

        public ITypeProperty Private(CodeTypeReference type, string name,
            out CodePropertyReferenceExpression propertyReference)
        {
            var property = new CodeMemberProperty
            {
                Type = type,
                Name = name,
                Attributes = MemberAttributes.Private | MemberAttributes.Final
            };
            _properties.Add(property);
            return new PropertyBuilder(this, property, out propertyReference);
        }

        public CodeTypeMemberCollection Result()
        {
            return _properties;
        }

        public class PropertyBuilder : ITypeProperty
        {
            private readonly PropertiesBuilder _parent;
            private readonly CodeMemberProperty _property;
            private readonly CodePropertyReferenceExpression _propertyReference;

            public PropertyBuilder(PropertiesBuilder parent, CodeMemberProperty property,
                out CodePropertyReferenceExpression propertyReference)
            {
                _parent = parent;
                _property = property;
                _propertyReference = propertyReference =
                    new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), _property.Name);
            }

            public ITypeProperty Virtual()
            {
                _property.Attributes &= ~MemberAttributes.Final;
                return this;
            }

            public ITypeProperty Get(Statements statement)
            {
                _property.HasGet = true;
                statement?.Invoke(_property.GetStatements);
                return this;
            }

            public ITypeProperty Set(Statements statement)
            {
                _property.HasSet = true;
                statement?.Invoke(_property.SetStatements);
                return this;
            }
        }
    }
}