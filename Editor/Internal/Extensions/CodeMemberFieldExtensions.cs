using System.CodeDom;

namespace ScriptGenerator.Editor.Internal
{
    internal static class CodeMemberFieldExtensions
    {
        public static CodeMemberField MakeReadonly(this CodeMemberField memberField, out CodeMemberField readonlyField)
        {
            readonlyField = new CodeMemberField($"readonly {memberField.Type.BaseType}", memberField.Name);
            return memberField;
        }
    }
}