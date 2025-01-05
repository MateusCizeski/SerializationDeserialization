using System.Text;

namespace Services.Services
{
    public class SoapService
    {
        public static string Serializar<T>(T objeto)
        {
            var formatter = new SoapFormatter();
            using var memoryStream = new MemoryStream();
            formatter.Serialize(memoryStream, objeto);
            return Encoding.UTF8.GetString(memoryStream.ToArray());
        }

        public static T Deserializar<T>(Stream stream)
        {
            var formatter = new SoapFormatter();
            return (T)formatter.Deserialize(stream);
        }
    }
}
