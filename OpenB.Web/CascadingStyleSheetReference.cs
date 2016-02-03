namespace OpenB.Web
{
    public class CascadingStyleSheetReference 
    {
        public string XPath { get { return "/html/head/"; } }
        public string NodeContents { get { return $"<link rel=\"stylesheet\" type=\"text/css\" href=\"{ScriptFile}\">"; } }
        public string ScriptFile { get; private set; }

        public CascadingStyleSheetReference(string scriptFile)
        {
            ScriptFile = scriptFile;
        }
    }
}