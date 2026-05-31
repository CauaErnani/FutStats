using FutStats.Data;
using FutStats.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FutStats.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClassificacaoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClassificacaoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Classificacao>>> GetClassificacao()
        {
            // 1. Buscamos todos os times e todas as partidas do banco
            var times = await _context.Times.ToListAsync();
            var partidas = await _context.Partidas.ToListAsync();

            var tabela = new List<Classificacao>();

            // 2. Para cada time, calculamos as estatísticas
            foreach (var time in times)
            {
                var stats = new Classificacao { NomeTime = time.Nome };

                // Encontra os jogos onde este time foi o Mandante (Casa)
                var partidasCasa = partidas.Where(p => p.IdTimeCasa == time.Id);
                foreach (var p in partidasCasa)
                {
                    stats.Jogos++;
                    stats.GolsFeitos += p.PlacarTimeCasa;
                    stats.GolsSofridos += p.PlacarTimeFora;

                    if (p.PlacarTimeCasa > p.PlacarTimeFora) { stats.Vitorias++; stats.Pontos += 3; }
                    else if (p.PlacarTimeCasa == p.PlacarTimeFora) { stats.Empates++; stats.Pontos += 1; }
                    else { stats.Derrotas++; }
                }

                // Encontra os jogos onde este time foi o Visitante (Fora)
                var partidasFora = partidas.Where(p => p.IdTimeFora == time.Id);
                foreach (var p in partidasFora)
                {
                    stats.Jogos++;
                    stats.GolsFeitos += p.PlacarTimeFora;
                    stats.GolsSofridos += p.PlacarTimeCasa;

                    if (p.PlacarTimeFora > p.PlacarTimeCasa) { stats.Vitorias++; stats.Pontos += 3; }
                    else if (p.PlacarTimeFora == p.PlacarTimeCasa) { stats.Empates++; stats.Pontos += 1; }
                    else { stats.Derrotas++; }
                }

                tabela.Add(stats);
            }

            // 3. Ordenação pesada usando LINQ (Exatamente a sua regra de negócio original)
            var tabelaOrdenada = tabela
                .OrderByDescending(c => c.Pontos)
                .ThenByDescending(c => c.Vitorias)
                .ThenByDescending(c => c.SaldoGols)
                .ThenByDescending(c => c.GolsFeitos)
                .ToList();

            // 4. Define a posição (1º, 2º, 3º...) baseada na ordenação final
            for (int i = 0; i < tabelaOrdenada.Count; i++)
            {
                tabelaOrdenada[i].Posicao = i + 1;
            }

            return tabelaOrdenada;
        }
    }
}