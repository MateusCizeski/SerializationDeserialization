using Domain.Entities;
using System.Text.Json;

namespace Services.Services
{
    public class JsonService
    {
        public static string Serializar(List<Veiculo> veiculos)
        {
            return JsonSerializer.Serialize(veiculos, new JsonSerializerOptions { WriteIndented = true });
        }

        public static List<Veiculo> Desserializar(string json)
        {
            return JsonSerializer.Deserialize<List<Veiculo>>(json);
        }
    }
}
