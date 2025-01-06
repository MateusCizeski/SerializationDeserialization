using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Services.Services
{
    public class SoapService
    {
        private const string SoapEnvelopeNamespace = "http://schemas.xmlsoap.org/soap/envelope/";

        public static string Serializar<T>(T objeto)
        {
            var serializer = new XmlSerializer(typeof(T));
            using var stringWriter = new StringWriter();
            using var xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings { Indent = true });

            // Inicia o SOAP Envelope
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("soap", "Envelope", SoapEnvelopeNamespace);

            // Especifica o Body do SOAP
            xmlWriter.WriteStartElement("soap", "Body", SoapEnvelopeNamespace);

            // Serializa o objeto no corpo do SOAP
            serializer.Serialize(xmlWriter, objeto);

            // Fecha as tags corretamente
            xmlWriter.WriteEndElement(); // Fecha o Body
            xmlWriter.WriteEndElement(); // Fecha o Envelope
            xmlWriter.WriteEndDocument(); // Fecha o documento XML

            return stringWriter.ToString();
        }

        public static T Desserializar<T>(Stream stream)
        {
            var document = new XmlDocument();

            // Carrega o conteúdo do stream XML
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                document.Load(reader);
            }

            // Localiza a tag <Body> dentro do SOAP Envelope
            var soapBody = document.DocumentElement["Body"]?.FirstChild;

            if (soapBody == null)
            {
                throw new InvalidOperationException("SOAP Body não encontrado no XML.");
            }

            // Desserializa o conteúdo do SOAP Body
            var serializer = new XmlSerializer(typeof(T));
            using var reader2 = new XmlNodeReader(soapBody);
            return (T)serializer.Deserialize(reader2);
        }
    }
}
