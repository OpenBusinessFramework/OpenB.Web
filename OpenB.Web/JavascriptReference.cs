namespace OpenB.Web
{
    public class JavascriptReference
    {   
        public string XPath { get { return "/html/head/"; } }
        public string NodeContents { get { return $"script src=\"{ScriptFile}\"></script>"; } }
        public string ScriptFile { get; private set; }

        public JavascriptReference(string scriptFile)
        {
            ScriptFile = scriptFile;
        }
    }
}