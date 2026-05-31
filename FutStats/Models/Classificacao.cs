namespace FutStats.Models
{
    public class Classificacao
    {
        public int Posicao { get; set; }
        public string NomeTime { get; set; } = string.Empty;
        public int Pontos { get; set; }
        public int Jogos { get; set; }
        public int Vitorias { get; set; }
        public int Empates { get; set; }
        public int Derrotas { get; set; }
        public int GolsFeitos { get; set; }
        public int GolsSofridos { get; set; }

        // No C#, podemos criar propriedades dinâmicas usando "=>". 
        // O Saldo de Gols será sempre calculado na hora!
        public int SaldoGols => GolsFeitos - GolsSofridos;
    }
}