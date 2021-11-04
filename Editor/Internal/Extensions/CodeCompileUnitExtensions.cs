using System.CodeDom;

namespace ScriptGenerator.Editor.Internal
{
    internal static class CodeCompileUnitExtensions
    {
        public static NamespaceBuilder Globals(this CodeCompileUnit targetUnit)
        {
            if (targetUnit.Namespaces.Count < 1 || !string.IsNullOrEmpty(targetUnit.Namespaces[0].Name))
            {
                var ns = new CodeNamespace("");
                targetUnit.Namespaces.Insert(0, ns);
            }

            return new NamespaceBuilder(targetUnit.Namespaces[0]);
        }

        public static NamespaceBuilder WithNamespace(this CodeCompileUnit targetUnit, string ns)
        {
            var codeNs = new CodeNamespace(ns);
            targetUnit.Namespaces.Add(codeNs);
            return new NamespaceBuilder(codeNs);
        }
    }
}