using FluentValidation;
using FutStats.Data;
using FutStats.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FutStats.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PartidasController : ControllerBase
    {
        private readonly AppDbContext _context;

        // Injetando o banco de dados
        public PartidasController(AppDbContext context)
        {
            _context = context;
        }

        // ROTA GET: Lista as partidas com Paginação e Filtros
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Partida>>> GetPartidas(
            [FromQuery] int pagina = 1,
            [FromQuery] int tamanhoPagina = 10,
            [FromQuery] int? timeId = null) // O "?" significa que o filtro é opcional
        {
            // Preparamos a consulta base (sem ir ao banco de dados ainda)
            var query = _context.Partidas.AsQueryable();

            // 1. Aplica o filtro de Time (se o usuário mandou um timeId na requisição)
            if (timeId.HasValue)
            {
                query = query.Where(p => p.IdTimeCasa == timeId || p.IdTimeFora == timeId);
            }

            // 2. Aplica a Paginação usando a lógica de pular e pegar
            var partidas = await query
                .Skip((pagina - 1) * tamanhoPagina)
                .Take(tamanhoPagina)
                .ToListAsync(); // Só agora ele vai no banco de dados e traz o resultado enxuto

            return Ok(partidas);
        }

        [HttpPost]
        public async Task<ActionResult<Partida>> PostPartida(
    Partida partida,
    [FromServices] IValidator<Partida> validador) // Injetando o validador aqui
        {
            // 1. Roda as regras de negócio
            var resultadoValidacao = await validador.ValidateAsync(partida);

            // 2. Se a validação falhar, barra a requisição e devolve os erros
            if (!resultadoValidacao.IsValid)
            {
                return BadRequest(resultadoValidacao.Errors.Select(e => e.ErrorMessage));
            }

            // 3. Se passou, salva no banco normalmente
            _context.Partidas.Add(partida);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPartidas), new { id = partida.Id }, partida);
        }
    }
}