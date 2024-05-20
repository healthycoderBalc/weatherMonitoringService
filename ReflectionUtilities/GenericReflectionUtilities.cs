using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace weatherMonitoringService.ReflectionUtilities
{
    public static class GenericReflectionUtilities
    {
        public static IEnumerable<Type> GetClassesImplementingAnInterface(Type interfaceType)
        {
            IEnumerable<Type> classesImplementingType = AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes().Where(type => interfaceType.IsAssignableFrom(type) && type.IsClass && !type.IsAbstract));

            return classesImplementingType;
        }

        public static IEnumerable<PropertyInfo> GetPropertiesOfAClass<T>()
        {
            Type targetType = typeof(T);
            List<PropertyInfo> properties = [];

            AddPropertiesFromType(targetType, properties);

            return properties;
        }

        private static void AddPropertiesFromType(Type type, List<PropertyInfo> properties)
        {
            PropertyInfo[] currentProperties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            properties.AddRange(currentProperties);

            Type[] interfaces = type.GetInterfaces();
            foreach (Type interfaceType in interfaces)
            {
                AddPropertiesFromType(interfaceType, properties);
            }

            Type? baseType = type.BaseType;
            if (baseType != null && baseType != typeof(object))
            {
                AddPropertiesFromType(baseType, properties);
            }
        }
    }
}
