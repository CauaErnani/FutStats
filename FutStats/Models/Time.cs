namespace FutStats.Models
{
    public class Time
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public DateTime? Fundacao { get; set; }
    }
}
