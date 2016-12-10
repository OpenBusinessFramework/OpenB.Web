using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OpenB.Web.Util.Reflection
{
    public class Reflector
    {
        public static Type[] GetTypes(Assembly assembly, TypeSearchOptions typeSearchOptions)
        {
            if (assembly == null)
                throw new ArgumentNullException(nameof(assembly));
            if (typeSearchOptions == null)
                throw new ArgumentNullException(nameof(typeSearchOptions));


            ICollection<Type> resultList = new List<Type>();

            foreach (Type interfaceType in typeSearchOptions.ImplementsInterfaces)
            {
                assembly.GetTypes().Where(t => interfaceType.IsAssignableFrom(t) && t.IsAbstract == false && t.IsInterface == false).ToList().ForEach(i => resultList.Add(i));
            }

            return resultList.ToArray();
        }
    }

    public class TypeSearchOptions
    {
        public IList<Type> ImplementsInterfaces { get; set; }

        public TypeSearchOptions()
        {
            ImplementsInterfaces = new List<Type>();
        }
    }
}
