namespace OpenB.Web.Content.Elements
{
    public interface IElement
    {
        string AggregatedKey
        {
            get;
        }       

        IElementContainer Parent { get; }
    }
}