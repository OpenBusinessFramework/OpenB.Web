using System.Collections.Generic;

namespace OpenB.Web.Content.Elements
{
    public interface IElementContainer : IElement
    {
        IList<IElement> Elements
        {
            get;
        }
    }
}