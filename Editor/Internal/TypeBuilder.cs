using System;
using System.CodeDom;
using System.Reflection;
using JetBrains.Annotations;

namespace ScriptGenerator.Editor.Internal
{
    internal class TypeBuilder : ITypeDeclaration, ITypeMembers
    {
        internal readonly CodeTypeDeclaration typeDeclaration;
        [CanBeNull] private CodeTypeReference _baseType;

        [CanBeNull] internal CodeConstructor codeConstructor;

        public TypeBuilder()
        {
            typeDeclaration = new CodeTypeDeclaration();
        }

        public ITypeDeclaration Name(string name)
        {
            typeDeclaration.Name = name;
            return this;
        }

        public ITypeDeclaration IsClass()
        {
            typeDeclaration.IsClass = true;
            return this;
        }

        public ITypeDeclaration TypeAttributes(TypeAttributes typeAttributes)
        {
            typeDeclaration.TypeAttributes = typeAttributes;
            return this;
        }

        public ITypeDeclaration IsPartial()
        {
            typeDeclaration.IsPartial = true;
            return this;
        }

        public ITypeDeclaration Inherits(CodeTypeReference type)
        {
            if (_baseType != null)
            {
                typeDeclaration.BaseTypes.Remove(_baseType);
            }

            _baseType = type;
            typeDeclaration.BaseTypes.Add(type);
            return this;
        }

        public ITypeDeclaration Implements(CodeTypeReference type)
        {
            if (!typeDeclaration.BaseTypes.Contains(type))
            {
                typeDeclaration.BaseTypes.Add(type);
            }

            return this;
        }

        public ITypeDeclaration IsEnum()
        {
            typeDeclaration.IsEnum = true;
            return this;
        }

        public ITypeMembers Members()
        {
            return this;
        }

        public CodeTypeDeclaration Result()
        {
            return typeDeclaration;
        }

        public ITypeMembers AddNestedType(Action<ITypeDeclaration> build,
            out CodeTypeReference typeReference)
        {
            var builder = new TypeBuilder();
            build?.Invoke(builder);
            var nestedTypeDeclaration = builder.Result();
            typeDeclaration.Members.Add(nestedTypeDeclaration);
            typeReference = new CodeTypeReference(nestedTypeDeclaration.Name);
            return this;
        }

        public ITypeMembers Constructor(Action<ITypeConstructor> build)
        {
            var builder = new TypeConstructorBuilder();
            build?.Invoke(builder);
            codeConstructor = builder.Result();
            typeDeclaration.Members.Add(codeConstructor!);
            return this;
        }

        public ITypeMembers Fields(Action<ITypeFields> build)
        {
            var builder = new TypeFieldsBuilder(this);
            build?.Invoke(builder);
            typeDeclaration.Members.AddRange(builder.Result());
            return this;
        }

        public ITypeMembers Properties(Action<ITypeProperties> build)
        {
            var builder = new TypePropertiesBuilder(this);
            build?.Invoke(builder);
            typeDeclaration.Members.AddRange(builder.Result());
            return this;
        }

        public ITypeMembers EnumFields(Action<IEnumFields> build)
        {
            var builder = new EnumFieldsBuilder(this);
            build?.Invoke(builder);
            typeDeclaration.Members.AddRange(builder.Result());
            return this;
        }

        public ITypeMembers Methods(Action<ITypeMethods> build)
        {
            var builder = new TypeMethodsBuilder(this);
            build?.Invoke(builder);
            typeDeclaration.Members.AddRange(builder.Result());
            return this;
        }
    }
}