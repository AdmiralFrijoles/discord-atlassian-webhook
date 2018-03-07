using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DiscordAtlassianWebhook
{
    internal static class ReflectionHelpers
    {
        public static IDictionary<Type, Attribute[]> GetTypesWithAttribute(Assembly assembly, Type attributeType)
        {
            return assembly.GetTypes()
                .Select(i => new
                {
                    Type = i,
                    Attributes = (Attribute[]) i.GetCustomAttributes(attributeType, true)
                }).Where(i => i.Attributes.Length > 0)
                .ToDictionary(i => i.Type, i => i.Attributes);
        }
        public static IDictionary<Type, T[]> GetTypesWithAttribute<T>(Assembly assembly) where T : Attribute
        {
            return assembly.GetTypes()
                .Select(i => new
                {
                    Type = i,
                    Attributes = (T[]) i.GetCustomAttributes(typeof(T), true)
                })
                .Where(i => i.Attributes.Length > 0)
                .ToDictionary(i => i.Type, i => i.Attributes);
        }

        public static IDictionary<MethodInfo, Attribute[]> GetMethodsWithAttribute(Type objectType, Type attributeType)
        {
            return objectType.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Select(i => new
                {
                    Method = i,
                    Attributes = (Attribute[])i.GetCustomAttributes(attributeType, true)
                })
                .Where(i => i.Attributes.Length > 0)
                .ToDictionary(i => i.Method, i => i.Attributes);
        }
        public static IDictionary<MethodInfo, T[]> GetMethodsWithAttribute<T>(Type objectType) where T : Attribute
        {
            return objectType.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Select(i => new
                {
                    Method = i,
                    Attributes = (T[]) i.GetCustomAttributes(typeof(T), true)
                })
                .Where(i => i.Attributes.Length > 0)
                .ToDictionary(i => i.Method, i => i.Attributes);
        }

    }
}