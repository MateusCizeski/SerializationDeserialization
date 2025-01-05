using System.Runtime.Serialization.Formatters.Binary;

namespace Services.Services
{
    public class BinarioService
    {
        public static byte[] Serializar<T>(T objeto)
        {
            var formatter = new BinaryFormatter();
            using var memoryStream = new MemoryStream();
            formatter.Serialize(memoryStream, objeto);
            return memoryStream.ToArray();
        }

        public static T Deserializar<T>(Stream stream)
        {
            var formatter = new BinaryFormatter();
            return (T)formatter.Deserialize(stream);
        }
    }
}
