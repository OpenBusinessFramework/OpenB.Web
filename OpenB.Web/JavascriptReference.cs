using System;

namespace OpenB.Web
{
    public class JavascriptReference : IWebReference
    {   
        public string XPath { get { return "/html/head/"; } }
        public string ScriptFile { get; private set; }
        public string ScriptFolder { get { return "js"; } }
         

        public JavascriptReference(string scriptFile)
        {           
            ScriptFile = scriptFile;
        }

        public string GetWebReference(string host)
        {
            return $"<script src=\"{host}/{ScriptFolder}/{ScriptFile}\"></script>";
        }
    }

    public interface IWebReference
    {
        string XPath { get; }     
        string ScriptFile { get; }       
        string ScriptFolder { get; }

        string GetWebReference(string host);
    }
}