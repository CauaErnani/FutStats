namespace FutStats.Models
{
    public class Partida
    {
        public int Id { get; set; }
        public int IdTimeCasa { get; set; }
        public int IdTimeFora { get; set; }
        public DateTime DataHora { get; set; }
        public string Estadio { get; set; } = string.Empty;
        public int PlacarTimeCasa { get; set; }
        public int PlacarTimeFora { get; set; }
    }
}
