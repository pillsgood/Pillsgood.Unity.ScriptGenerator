using System;
using System.CodeDom;
using System.Reflection;
using JetBrains.Annotations;

namespace ScriptGenerator.Editor.Internal
{
    internal class CodeTypeBuilder : ICodeTypeDeclaration, ICodeTypeMembers
    {
        private readonly CodeTypeDeclaration _codeTypeDeclaration;
        [CanBeNull] private CodeTypeReference _baseType;

        [CanBeNull] internal CodeConstructor codeConstructor;

        public CodeTypeBuilder()
        {
            _codeTypeDeclaration = new CodeTypeDeclaration();
        }

        public ICodeTypeDeclaration Name(string name)
        {
            _codeTypeDeclaration.Name = name;
            return this;
        }

        public ICodeTypeDeclaration IsClass()
        {
            _codeTypeDeclaration.IsClass = true;
            return this;
        }

        public ICodeTypeDeclaration TypeAttributes(TypeAttributes typeAttributes)
        {
            _codeTypeDeclaration.TypeAttributes = typeAttributes;
            return this;
        }

        public ICodeTypeDeclaration IsPartial()
        {
            _codeTypeDeclaration.IsPartial = true;
            return this;
        }

        public ICodeTypeDeclaration Inherits(CodeTypeReference type)
        {
            if (_baseType != null) _codeTypeDeclaration.BaseTypes.Remove(_baseType);
            _baseType = type;
            _codeTypeDeclaration.BaseTypes.Add(type);
            return this;
        }

        public ICodeTypeDeclaration Implements(CodeTypeReference type)
        {
            if (!_codeTypeDeclaration.BaseTypes.Contains(type)) _codeTypeDeclaration.BaseTypes.Add(type);

            return this;
        }

        public ICodeTypeMembers Members()
        {
            return this;
        }

        public CodeTypeDeclaration Result()
        {
            return _codeTypeDeclaration;
        }

        public ICodeTypeMembers AddNestedType(Action<ICodeTypeDeclaration> build)
        {
            return AddNestedType(build, out _);
        }

        public ICodeTypeMembers AddNestedType(Action<ICodeTypeDeclaration> build,
            out CodeTypeReference typeReference)
        {
            var builder = new CodeTypeBuilder();
            build?.Invoke(builder);
            var typeDeclaration = builder.Result();
            _codeTypeDeclaration.Members.Add(typeDeclaration);
            typeReference = new CodeTypeReference(typeDeclaration.Name);
            return this;
        }

        public ICodeTypeMembers Constructor(Action<ITypeConstructor> build)
        {
            var builder = new ConstructorBuilder();
            build?.Invoke(builder);
            codeConstructor = builder.Result();
            _codeTypeDeclaration.Members.Add(codeConstructor!);
            return this;
        }

        public ICodeTypeMembers Fields(Action<ITypeFields> build)
        {
            var builder = new FieldsBuilder(this);
            build?.Invoke(builder);
            _codeTypeDeclaration.Members.AddRange(builder.Result());
            return this;
        }

        public ICodeTypeMembers Fields<T>(out T arg, Action<ITypeFields> build)
        {
            arg = default;
            return Fields(build);
        }

        public ICodeTypeMembers Fields<T1, T2>(out T1 arg1, out T2 arg2, Action<ITypeFields> build)
        {
            arg1 = default;
            arg2 = default;
            return Fields(build);
        }

        public ICodeTypeMembers Properties(Action<ITypeProperties> build)
        {
            var builder = new PropertiesBuilder(this);
            build?.Invoke(builder);
            _codeTypeDeclaration.Members.AddRange(builder.Result());
            return this;
        }
    }
}