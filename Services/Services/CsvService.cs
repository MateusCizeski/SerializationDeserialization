using System.Text;

namespace Services.Services
{
    public class CsvService
    {
        public static string Serializar<T>(List<T> objetos) where T : class
        {
            var csvBuilder = new StringBuilder();
            var properties = typeof(T).GetProperties();

            csvBuilder.AppendLine(string.Join(",", properties.Select(p => p.Name)));

            foreach(var obj in objetos)
            {
                var line = string.Join(",", properties.Select(p => p.GetValue(obj)?.ToString() ?? ""));
                csvBuilder.AppendLine(line);
            }

            return csvBuilder.ToString();
        }

        public static List<T> Desserializar<T>(Stream stream) where T : class, new()
        {
            var reader = new StreamReader(stream);
            var lines = reader.ReadToEnd().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            var properties = typeof(T).GetProperties();
            var data = new List<T>();

            foreach (var line in lines.Skip(1))
            {
                var values = line.Split(",");
                var obj = new T();

                for(int i = 0; i < properties.Length; i++)
                {
                    var property = properties[i];
                    
                    if(property.CanWrite)
                    {
                        var value = Convert.ChangeType(values[i], property.PropertyType);
                        property.SetValue(obj, value);
                    }
                }

                data.Add(obj);
            }

            return data;
        }
    }
}
