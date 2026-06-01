using FutStats.Data;
using FutStats.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FluentValidation; // Adicionado para usar a validação

namespace FutStats.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TimesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TimesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Time>>> GetTimes()
        {
            return await _context.Times.ToListAsync();
        }

        // ROTA POST: Agora blindada com validação
        [HttpPost]
        public async Task<ActionResult<Time>> PostTime(
            Time time,
            [FromServices] IValidator<Time> validador) // Puxando o validador de Times
        {
            // 1. Roda as regras de negócio
            var resultadoValidacao = await validador.ValidateAsync(time);

            // 2. Se falhar, barra e devolve os erros
            if (!resultadoValidacao.IsValid)
            {
                return BadRequest(resultadoValidacao.Errors.Select(e => e.ErrorMessage));
            }

            // 3. Se passar, salva no banco
            _context.Times.Add(time);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTimes), new { id = time.Id }, time);
        }
    }
}