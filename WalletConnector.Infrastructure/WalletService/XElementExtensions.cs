using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace WalletConnector.Infrastructure
{
    public static class XElementExtensions
    {
        public static T FromXElement<T>(this string xelement)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xelement);

            XmlSerializer serializer = new XmlSerializer(typeof(T));
            StringReader stringReader = new StringReader(xelement);

            return (T)serializer.Deserialize(stringReader);
        }

        public static XElement ToXElement<T>(this T obj)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (TextWriter streamWriter = new StreamWriter(memoryStream))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                    xmlSerializer.Serialize(streamWriter, obj);
                    return XElement.Parse(Encoding.UTF8.GetString(memoryStream.ToArray()));
                }
            }
        }
    }
}
