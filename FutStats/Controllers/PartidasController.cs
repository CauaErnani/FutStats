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

        // ROTA GET: Lista todas as partidas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Partida>>> GetPartidas()
        {
            return await _context.Partidas.ToListAsync();
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