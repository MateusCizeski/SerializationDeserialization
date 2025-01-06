using Domain.Entities;
using System.Xml.Serialization;

namespace Services.Services
{
    public class XmlService
    {
        public static string Serializar(List<Veiculo> veiculos)
        {
            var serializer = new XmlSerializer(typeof(List<Veiculo>));
            using var stringWriter = new StringWriter();
            serializer.Serialize(stringWriter, veiculos);
            return stringWriter.ToString();
        }

        public static List<Veiculo> Desserializar(string xml)
        {
            var serializer = new XmlSerializer(typeof(List<Veiculo>));
            using var stringReader = new StringReader(xml);
            return (List<Veiculo>)serializer.Deserialize(stringReader);
        }
    }
}
