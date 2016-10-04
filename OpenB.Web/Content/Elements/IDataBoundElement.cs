namespace OpenB.Web.Content.Elements
{
    public interface IDataBoundElement
    {
         /// <summary>
         /// The model to bind the <see cref="IElement"/> object to.
         /// </summary>
         string Model { get; set; }
    }
}