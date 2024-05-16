using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace weatherMonitoringService.WeatherStations.FileFormats
{
    public static class FormatManager
    {
        public static string? DetectFileFormat(string? inputText, Type interfaceType, string methodName)
        {
            IEnumerable<Type> classesImplementingThisInterface = GetClassesImplementingAnInterface(interfaceType);
            if (!classesImplementingThisInterface.Any()) return null;
            foreach (Type type in classesImplementingThisInterface)
            {
                if (CheckCurrentFormat(type, methodName, inputText, out string? result))
                {
                    return result;
                }
            }
            return null;
        }

        private static IEnumerable<Type> GetClassesImplementingAnInterface(Type interfaceType)
        {
            IEnumerable<Type> formatDetectorTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes().Where(type => interfaceType.IsAssignableFrom(type) && type.IsClass && !type.IsAbstract));

            return formatDetectorTypes;
        }

        private static bool CheckCurrentFormat(Type type, string methodName, string? inputText, out string? result)
        {
            result = null;
            MethodInfo? requiredMethod = type.GetMethod(methodName);

            if (requiredMethod == null || requiredMethod.ReturnType != typeof(bool)) return false;

            object? instance = null;
            if (!requiredMethod.IsStatic)
            {
                instance = Activator.CreateInstance(type);
            }

            bool returnOfMethod = (bool)requiredMethod.Invoke(instance, new object[] { inputText });
            if (returnOfMethod)
            {
                result = type.Name;
                return true;
            }
            return false;
        }
    }
}
