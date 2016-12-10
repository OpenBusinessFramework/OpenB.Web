﻿using OpenB.Core.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace OpenB.Web.Util
{

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
}