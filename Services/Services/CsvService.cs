using CsvHelper;
using Domain.Entities;
using System.Globalization;

namespace Services.Services
{
    public class CsvService
    {
        public static string Serializar(List<Veiculo> veiculos)
        {
            using var writer = new StringWriter();
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.WriteRecords(veiculos);
            return writer.ToString();
        }

        public static List<Veiculo> Desserializar(Stream stream)
        {
            using var reader = new StreamReader(stream);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var records = csv.GetRecords<Veiculo>().ToList();
            return records;
        }
    }
}
