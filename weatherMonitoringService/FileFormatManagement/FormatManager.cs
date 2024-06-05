using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using weatherMonitoringService.ConsoleInterface;
using weatherMonitoringService.ReflectionUtilities;

namespace weatherMonitoringService.FileFormatManagement
{
    public static class FormatManager
    {
        public static string DetectFileFormat(string? inputText, Type interfaceType, string methodName)
        {
            IEnumerable<Type> classesImplementingThisInterface = GenericReflectionUtilities.GetClassesImplementingAnInterface(interfaceType);
            if (!classesImplementingThisInterface.Any())
            {
                ConsoleErrorMessages.NoClassesImplementingInterface();
                return string.Empty;
            }
            foreach (Type type in classesImplementingThisInterface)
            {
                if (CheckCurrentFormat(type, methodName, inputText, out string? result))
                {
                    return result ?? string.Empty;
                }
            }
            ConsoleErrorMessages.NoMatchingFormat();
            return string.Empty;
        }

        private static bool CheckCurrentFormat(Type type, string methodName, string? inputText, out string? result)
        {
            result = null;
            MethodInfo? requiredMethod = type.GetMethod(methodName);

            if (requiredMethod == null || requiredMethod.ReturnType != typeof(bool)) return false;

            object? instance = null;
            if (!requiredMethod.IsStatic) instance = Activator.CreateInstance(type);
            if (instance == null && !requiredMethod.IsStatic) return false;

            object? returnValue = requiredMethod.Invoke(instance, [inputText]);
            if (returnValue is bool returnOfMethod && returnOfMethod)
            {
                result = type.Name;
                return true;
            };
            return false;
        }
    }
}
