namespace OpenB.Web
{
    public class Configuration : IConfiguration
    {
        public IWebSolutionConfiguration WebSolution { get; set; }
    }

    public interface IConfiguration
    {
    }
}
