using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Config
{
    public static class Import
    {
        public static Config LoadConfigFromXmlFile(string filename)
        {
            var xDocument = XDocument.Load(filename);
            return CreateObjectsFromString<Config>(xDocument);
        }

        private static T CreateObjectsFromString<T>(XDocument xDocument)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            return (T) xmlSerializer.Deserialize(new StringReader(xDocument.ToString()));
        }
    }
}