namespace Domain.Entities
{
    public class Veiculo
    {
        public string Marca { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public int Ano { get; set; }
        public double Preco { get; set; }
    }
}
