namespace OpenB.Web
{
    public class CascadingStyleSheetReference : IWebReference
    {
        public string XPath { get { return "/html/head/"; } }       
        public string ScriptFile { get; }        
        public string ScriptFolder
        {
            get
            {
                return "css";
            }
        }

        public string GetWebReference(string host)
        {
            return $"<link rel=\"stylesheet\" type=\"text/css\" href=\"{host}/{ScriptFolder}/{ScriptFile}\">";
        }

        public CascadingStyleSheetReference(string scriptFile)
        {
            ScriptFile = scriptFile;
        }
    }
}