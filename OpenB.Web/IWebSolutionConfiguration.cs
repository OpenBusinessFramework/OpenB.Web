namespace OpenB.Web
{
    public interface IWebSolutionConfiguration
    {   
        string Name { get; }
        IWebPackage WebPackage { get; }
        string DefaultDomainModelNamespace { get; }
    }
}