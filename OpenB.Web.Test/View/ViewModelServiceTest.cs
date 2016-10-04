using Microsoft.CSharp;
using NUnit.Framework;
using OpenB.Core;
using OpenB.Core.ACL;
using OpenB.Core.Utils;
using OpenB.Web.View;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace OpenB.Web.Test.View
{
    public class PageGenerationService
    {

    }

    public class CSharpClassBuilder : IClassStringBuilder
    {
        private readonly FormattedStringBuilder formattedStringBuilder;
        readonly string nameSpace;
        readonly string name;

        readonly IList<PropertyInfo> properties;
        readonly Type interfaceName;


        public CSharpClassBuilder(string nameSpace, string className, Type interfaceName)
        {
            this.interfaceName = interfaceName;
            if (interfaceName == null)
                throw new ArgumentNullException(nameof(interfaceName));

            if (nameSpace == null)
                throw new ArgumentNullException(nameof(nameSpace));

            formattedStringBuilder = new FormattedStringBuilder();

            this.nameSpace = nameSpace;
            this.name = className;
            this.properties = new List<PropertyInfo>();
        }

        private void BeginSection()
        {
            formattedStringBuilder.AppendLine("{");
            formattedStringBuilder.LevelDown();
        }

        private void EndSection()
        {
            formattedStringBuilder.LevelUp();
            formattedStringBuilder.AppendLine("}");
        }

        public string Build()
        {
            IList<string> usings = properties.Select(p => p.PropertyType.Namespace).Distinct().ToList();

            if (interfaceName != null)
            {
                usings.Add(interfaceName.Namespace);
            }

            foreach (var usingString in usings)
            {
                formattedStringBuilder.AppendLine($"using {usingString};");
            }

            formattedStringBuilder.AppendLine();

            formattedStringBuilder.AppendLine($"namespace {nameSpace}");
            BeginSection();

            formattedStringBuilder.AppendLine($"public class {name} : {interfaceName}");
            BeginSection();

            foreach (PropertyInfo propertySetting in this.properties)
            {
                formattedStringBuilder.AppendLine(GenerateProperty(propertySetting));
            }
            EndSection();
            EndSection();

            return formattedStringBuilder.ToString();

        }

        private static string GenerateProperty(PropertyInfo property)
        {
            string propertyString;
            if (typeof(IEnumerable).IsAssignableFrom(property.PropertyType) && property.PropertyType != typeof(string))
            {
                propertyString = $"public ViewModelCollection<{property.PropertyType.Name}> {property.PropertyType.Name} {property.Name} {{get; set;}}";
            }
            else
            {
                propertyString = $"public {property.PropertyType.Name} {property.Name} {{get; set;}}";
            }

            return propertyString;
        }

        public void AddProperty(PropertyInfo info)
        {
            this.properties.Add(info);
        }
    }

    public class ViewModelGenerationService
    {
        public IViewModel Generate(object model, MappingConfiguration mappingConfiguration)
        {
            Type modelType = model.GetType();
            string nameSpace = "ViewModels";

            if (mappingConfiguration == null)
                throw new ArgumentNullException(nameof(mappingConfiguration));



            string viewModelClass = string.Concat(modelType.Name, "ViewModel");

            string[] usings = new string[1];


            CSharpClassBuilder classBuilder = new CSharpClassBuilder(nameSpace, viewModelClass, typeof(IViewModel));

            var properties = model.GetType().GetProperties().Where(p => mappingConfiguration.Properties.Any(m => m.Name == p.Name));


            foreach (var property in properties)
            {

                classBuilder.AddProperty(property);

            }

            string classString = classBuilder.Build();

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

        private CompilerParameters GenerateParameters(string nameSpace)
        {
            var parameters = new CompilerParameters
            {
                GenerateExecutable = false,
                GenerateInMemory = true,
                OutputAssembly = nameSpace
            };

            // parameters.ReferencedAssemblies.Add("OpenB.Modeling.dll");
            parameters.ReferencedAssemblies.Add("OpenB.Core.dll");
            parameters.ReferencedAssemblies.Add("OpenB.Web.dll");
            return parameters;
        }


    }



    [TestFixture]
    public class ViewModelServiceTest
    {
        [Test]
        public void DoSomething()
        {

            TestModel user = new TestModel();

            ViewModelGenerationService service = new ViewModelGenerationService();
            MappingConfiguration mappingConfiguration = new MappingConfiguration(@"E:\Downloads\Projects\Projects\Personal\OpenB.Web\OpenB.Web.Test\View\Configuration\ViewModel.Mapping.xml");

            IViewModel resultModel = service.Generate(user, mappingConfiguration);


        }

    }

    public class TestModel : IModel
    {
        public string Description
        {
            get; set;
        }

        public bool IsActive
        {
            get; set;
        }

        public string Key
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }
    }
}
