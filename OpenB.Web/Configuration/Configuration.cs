namespace OpenB.Web
{
    public class ApplicationConfiguration : IConfiguration
    {
        internal string ConfigurationPath { get; set; }

        public WebSolutionConfiguration WebSolution { get; set; }
    }

    public interface IConfiguration
    {
    }
}
