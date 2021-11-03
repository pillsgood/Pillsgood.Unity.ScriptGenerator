using System;
using System.CodeDom;
using System.Reflection;
using JetBrains.Annotations;

namespace ScriptGenerator.Editor.Internal
{
    internal class CodeTypeBuilder : ICodeTypeDeclaration, ICodeTypeMembers
    {
        internal readonly CodeTypeDeclaration typeDeclaration;
        [CanBeNull] private CodeTypeReference _baseType;

        [CanBeNull] internal CodeConstructor codeConstructor;

        public CodeTypeBuilder()
        {
            typeDeclaration = new CodeTypeDeclaration();
        }

        public ICodeTypeDeclaration Name(string name)
        {
            typeDeclaration.Name = name;
            return this;
        }

        public ICodeTypeDeclaration IsClass()
        {
            typeDeclaration.IsClass = true;
            return this;
        }

        public ICodeTypeDeclaration TypeAttributes(TypeAttributes typeAttributes)
        {
            typeDeclaration.TypeAttributes = typeAttributes;
            return this;
        }

        public ICodeTypeDeclaration IsPartial()
        {
            typeDeclaration.IsPartial = true;
            return this;
        }

        public ICodeTypeDeclaration Inherits(CodeTypeReference type)
        {
            if (_baseType != null) typeDeclaration.BaseTypes.Remove(_baseType);
            _baseType = type;
            typeDeclaration.BaseTypes.Add(type);
            return this;
        }

        public ICodeTypeDeclaration Implements(CodeTypeReference type)
        {
            if (!typeDeclaration.BaseTypes.Contains(type)) typeDeclaration.BaseTypes.Add(type);

            return this;
        }

        public ICodeTypeDeclaration IsEnum()
        {
            typeDeclaration.IsEnum = true;
            return this;
        }

        public ICodeTypeMembers Members()
        {
            return this;
        }

        public CodeTypeDeclaration Result()
        {
            return typeDeclaration;
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
            var nestedTypeDeclaration = builder.Result();
            typeDeclaration.Members.Add(nestedTypeDeclaration);
            typeReference = new CodeTypeReference(nestedTypeDeclaration.Name);
            return this;
        }

        public ICodeTypeMembers Constructor(Action<ITypeConstructor> build)
        {
            var builder = new ConstructorBuilder();
            build?.Invoke(builder);
            codeConstructor = builder.Result();
            typeDeclaration.Members.Add(codeConstructor!);
            return this;
        }

        public ICodeTypeMembers Constructor<T>(out T arg, Action<ITypeConstructor> build)
        {
            arg = default;
            return Constructor(build);
        }

        public ICodeTypeMembers Fields(Action<ITypeFields> build)
        {
            var builder = new FieldsBuilder(this);
            build?.Invoke(builder);
            typeDeclaration.Members.AddRange(builder.Result());
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
            typeDeclaration.Members.AddRange(builder.Result());
            return this;
        }

        public ICodeTypeMembers EnumFields(Action<IEnumFields> build)
        {
            var builder = new EnumFieldsBuilder(this);
            build?.Invoke(builder);
            typeDeclaration.Members.AddRange(builder.Result());
            return this;
        }

        public ICodeTypeMembers Methods(Action<ITypeMethods> build)
        {
            var builder = new MethodsBuilder(this);
            build?.Invoke(builder);
            typeDeclaration.Members.AddRange(builder.Result());
            return this;
        }
    }
}