using System;
using System.IO;
using Newtonsoft.Json;

namespace DiscordAtlassianWebhook.Config
{
    public class ConfigReader : IConfigReader
    {
        private readonly string _configFilePath;
        private readonly string _sectionNameSuffix;

        private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
        {
            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
            ContractResolver = new ConfigReaderContractResolver(),
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        public ConfigReader(string configFilePath, string sectionNameSuffix = "Config")
        {
            _configFilePath = configFilePath;
            _sectionNameSuffix = sectionNameSuffix;
        }

        public T Load<T>() where T : class, new()
        {
            return Load(typeof(T)) as T;
        }

        public T LoadSection<T>() where T : class, new()
        {
            return LoadSection(typeof(T)) as T;
        }

        public object Load(Type type)
        {
            if (!File.Exists(_configFilePath))
            {
                return Activator.CreateInstance(type);
            }

            string jsonString = File.ReadAllText(_configFilePath);
            return JsonConvert.DeserializeObject(jsonString, type, SerializerSettings);
        }

        public object LoadSection(Type type)
        {
            if (!File.Exists(_configFilePath))
            {
                return Activator.CreateInstance(type);
            }

            string jsonString = File.ReadAllText(_configFilePath);
            string section = type.Name.Replace(_sectionNameSuffix, string.Empty).ToPascalCase();
            var data = JsonConvert.DeserializeObject<dynamic>(jsonString, SerializerSettings);
            var sectionData = data[section];

            return sectionData == null
                ? Activator.CreateInstance(type)
                : JsonConvert.DeserializeObject(JsonConvert.SerializeObject(sectionData), type, SerializerSettings);
        }
    }
}