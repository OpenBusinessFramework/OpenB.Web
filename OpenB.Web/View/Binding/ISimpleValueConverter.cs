namespace OpenB.Web.View.Binding
{
    public interface ISimpleValueConverter<TLeft, TRight> : ISimpleValueConverter
    {
        TLeft Convert(TRight value);
        TRight ConvertBack(TLeft value);
    }
}