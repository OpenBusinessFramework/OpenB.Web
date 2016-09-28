using NUnit.Framework;
using OpenB.Core.ACL;
using OpenB.Web.View;
using System;
using System.Collections;
using System.Collections.Generic;

namespace OpenB.Web.Test.View
{
    public class PageGenerationService
    {

    }

    public class ViewModelGenerationService
    {
        public IViewModel Generate(object model)
        {
            var properties = model.GetType().GetProperties();

            HashSet<string> namespaces = new HashSet<string>();

            foreach (var property in properties)
            {
                namespaces.Add(property.PropertyType.Namespace);

                string propertyString;
                if (typeof(IEnumerable).IsAssignableFrom(property.PropertyType) && property.PropertyType != typeof(string))
                {
                    propertyString = $"public ViewModelCollection<{property.PropertyType.Name}> {property.PropertyType.Name} {property.Name} {{get; set;}}";
                }
                else
                {
                    propertyString = $"public {property.PropertyType.Name} {property.Name} {{get; set;}}";
                }
            }




            throw new NotImplementedException();
        }


    }



    [TestFixture]
    class ViewModelServiceTest
    {
        [Test]
        public void DoSomething()
        {
            UserGroup userGroup = new UserGroup("UserGroup", "User group", "Nice usergroup");
            User user = new User("Test", userGroup);

            ViewModelGenerationService service = new ViewModelGenerationService();
            service.Generate(user);
        }
    }
}
