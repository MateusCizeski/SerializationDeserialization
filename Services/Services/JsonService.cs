using Domain.Entities;
using System.Text.Json;

namespace Services.Services
{
    public class JsonService
    {
        public static void SalvarComoJson(List<Veiculo> veiculos, string caminhoArquivo)
        {
            var json = JsonSerializer.Serialize(veiculos, new JsonSerializerOptions { WriteIndented = true});
            File.WriteAllText(caminhoArquivo, json);
        }

        public static List<Veiculo> CarregarDeJson(string caminhoArquivo)
        {
            var json = File.ReadAllText(caminhoArquivo);
            return JsonSerializer.Deserialize<List<Veiculo>>(json);
        }
    }
}
