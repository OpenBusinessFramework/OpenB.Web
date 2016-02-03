using System.Web.UI;

namespace OpenB.Web.Content
{
    public class RenderContext
    {
        public RenderContext(HtmlTextWriter textWriter)
        {
            HtmlTextWriter = textWriter;
        }

       public HtmlTextWriter HtmlTextWriter { get; }
    }
}