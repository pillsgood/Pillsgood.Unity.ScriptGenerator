using System.CodeDom;

namespace ScriptGenerator.Editor.Internal
{
    internal static class CodeCompileUnitExtensions
    {
        public static CodeNamespaceBuilder WithNamespace(this CodeCompileUnit targetUnit, string ns = "")
        {
            var codeNs = new CodeNamespace(ns);
            targetUnit.Namespaces.Add(codeNs);
            return new CodeNamespaceBuilder(codeNs);
        }
    }
}