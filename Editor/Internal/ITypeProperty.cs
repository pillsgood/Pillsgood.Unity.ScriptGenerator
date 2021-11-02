namespace ScriptGenerator.Editor.Internal
{
    internal interface ITypeProperty
    {
        ITypeProperty Virtual();

        ITypeProperty Get(Statements statement);
        ITypeProperty Set(Statements statement);
    }
}