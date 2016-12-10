using Microsoft.CSharp;
using OpenB.Core;
using OpenB.Web.Util;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

namespace OpenB.Web.View
{

    public class ViewModelGenerator
    {
        private string viewModelFolder;

        private Func<Assembly, string, bool, Type> typeResolver;
        private Func<AssemblyName, Assembly> assemblyResolver;

        public ViewModelGenerator(string viewModelFolder)
        {
            this.viewModelFolder = viewModelFolder;
        }

        /// <summary>
        /// Currently supporting only a single domain library.
        /// </summary>
        /// <param name="mappingConfigurations"></param>
        /// <returns></returns>
        public void GenerateAssembly(IList<MappingConfiguration> mappingConfigurations)
        {
            var domainLogic = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin", "MemMan.dll");

            if (!File.Exists(domainLogic))
            {
                throw new Exception($"Domain logic library could not be loaded: File not found ({domainLogic}).");
            }
            var viewModelsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin", "ViewModels.dll");


            if (File.Exists(viewModelsPath))
            {
                return;
            }

            string nameSpace = "ViewModels";

            IList<string> classes = new List<string>();


            var modelAssembly = Assembly.LoadFile(domainLogic);

            foreach (MappingConfiguration mappingConfiguration in mappingConfigurations)
            {

                Type modelType = modelAssembly.GetType(mappingConfiguration.Model);

                if (modelType == null)
                {
                    throw new NotSupportedException($"Cannot find model type {mappingConfiguration.Model}.");
                }

                string viewModelClass = string.Concat(modelType.Name, "ViewModel");
                var classString = GenerateClass(modelType, mappingConfiguration, nameSpace, viewModelClass);
                classes.Add(classString);
            }

            var codeProvider = new CSharpCodeProvider();
            CompilerParameters parameters = GenerateParameters(nameSpace);

            CompilerResults compilerResults = codeProvider.CompileAssemblyFromSource(parameters, classes.ToArray());

            if (compilerResults.Errors.Count > 0)
            {
                throw new Exception("Could not generate viewmodels.");
            }


        }


        public IViewModel Generate(Type modelType, MappingConfiguration mappingConfiguration)
        {

            string nameSpace = "ViewModels";

            if (mappingConfiguration == null)
                throw new ArgumentNullException(nameof(mappingConfiguration));



            string viewModelClass = string.Concat(modelType.Name, "ViewModel");

            string classString = GenerateClass(modelType, mappingConfiguration, nameSpace, viewModelClass);

            var codeProvider = new CSharpCodeProvider();
            CompilerParameters parameters = GenerateParameters(nameSpace);

            CompilerResults compilerResults = codeProvider.CompileAssemblyFromSource(parameters, classString);

            if (compilerResults.Errors.Count > 0)
            {
            }

            return
               compilerResults.CompiledAssembly.CreateInstance(
                   string.Format("{0}.{1}", nameSpace, viewModelClass), false, BindingFlags.CreateInstance,
                   null, null, CultureInfo.InvariantCulture, null) as IViewModel;

        }

        private static string GenerateClass(Type modelType, MappingConfiguration mappingConfiguration, string nameSpace, string viewModelClass)
        {
            string[] usings = new string[1];

            CSharpClassBuilder classBuilder = new CSharpClassBuilder(nameSpace, viewModelClass, typeof(IViewModel));

            var properties = modelType.GetProperties().Where(p => mappingConfiguration.Properties.Any(m => m.Name == p.Name));


            foreach (var property in properties)
            {
                classBuilder.AddProperty(property);
            }

            string classString = classBuilder.Build();
            return classString;
        }

        private CompilerParameters GenerateParameters(string nameSpace)
        {
            if (string.IsNullOrWhiteSpace(nameSpace))
                throw new ArgumentNullException(nameof(nameSpace));

            var tempPath = Path.GetTempPath();
            var binPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin");

            var parameters = new CompilerParameters
            {
                GenerateExecutable = false,
                GenerateInMemory = false,
                OutputAssembly = Path.Combine(binPath, "ViewModels.dll"),
                TempFiles = new TempFileCollection(tempPath, false),
            };

            parameters.ReferencedAssemblies.Add(Path.Combine(binPath, "OpenB.Core.dll"));
            parameters.ReferencedAssemblies.Add(Path.Combine(binPath, "OpenB.Web.dll"));
            parameters.ReferencedAssemblies.Add(Path.Combine(binPath, "MemMan.dll"));
            return parameters;
        }
    }

    internal class RepositoryManager
    {
        internal static IRepository<IModel> GetRepository<T>()
        {
            throw new NotImplementedException();
        }
    }

    internal interface IRepository<TModel> where TModel : IModel
    {
    }

    public interface ICommand
    {
    }
}