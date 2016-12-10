using OpenB.Web.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace OpenB.Web.Http
{
    public class WebRequestModule : IHttpModule
    {
        private ConfigurationFactory configurationFactory;

        public void Dispose()
        {

        }

        public WebRequestModule()
        {
            configurationFactory = ConfigurationFactory.GetInstance();
           
        }

        public void Init(HttpApplication context)
        {
            CreateOrUpdateViewModels();
        }

        private void CreateOrUpdateViewModels()
        {
            var viewModelFolder = Path.Combine(configurationFactory.ApplicationConfiguration.ConfigurationPath, "ViewModels");

            DirectoryInfo viewModelFolderInfo = new DirectoryInfo(viewModelFolder);

            FileInfo[] xmlFiles = viewModelFolderInfo.GetFiles("*.mapping.xml");

            IList<MappingConfiguration> mappings = new List<MappingConfiguration>();
            foreach (FileInfo fileInfo in xmlFiles)
            {
                mappings.Add(new MappingConfiguration(fileInfo.FullName));
            }

            ViewModelGenerator viewModelGenerator = new ViewModelGenerator(viewModelFolder);
            viewModelGenerator.GenerateAssembly(mappings);
        }

    }
}
