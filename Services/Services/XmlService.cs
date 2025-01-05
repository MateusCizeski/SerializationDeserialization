using Domain.Entities;
using System.Xml.Serialization;

namespace Services.Services
{
    public class XmlService
    {
        public static void SalvarComoXml(List<Veiculo> veiculos, string caminhoArquivo)
        {
            var serializer = new XmlSerializer(typeof(List<Veiculo>));
            using var writer = new StreamWriter(caminhoArquivo);
            serializer.Serialize(writer, veiculos);
        }

        public static List<Veiculo> CarregarDeXml(string caminhoArquivo)
        {
            var serializer = new XmlSerializer(typeof(List<Veiculo>));
            using var reader = new StreamReader(caminhoArquivo);
            return (List<Veiculo>)serializer.Deserialize(reader);
        }
    }
}
