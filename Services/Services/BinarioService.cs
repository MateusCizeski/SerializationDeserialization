using Domain.Entities;
using System.Runtime.Serialization.Formatters.Binary;

namespace Services.Services
{
    public class BinarioService
    {
        public static byte[] Serializar<T>(List<Veiculo> veiculos)
        {
            using var memoryStream = new MemoryStream();
            using var writer = new BinaryWriter(memoryStream);

            writer.Write(veiculos.Count);

            foreach (var veiculo in veiculos)
            {
                writer.Write(veiculo.Marca);
                writer.Write(veiculo.Modelo);
                writer.Write(veiculo.Ano);
                writer.Write(veiculo.Preco);
            }

            return memoryStream.ToArray();
        }

        public static List<Veiculo> Deserializar<T>(Stream stream)
        {
            var veiculos = new List<Veiculo>();
            using var reader = new BinaryReader(stream);

            var count = reader.ReadInt32();

            for (int i = 0; i < count; i++)
            {
                var marca = reader.ReadString();
                var modelo = reader.ReadString();
                var ano = reader.ReadInt32();
                var preco = reader.ReadDouble();

                veiculos.Add(new Veiculo
                {
                    Marca = marca,
                    Modelo = modelo,
                    Ano = ano,
                    Preco = preco
                });
            }


            return veiculos;
        }
    }
}
