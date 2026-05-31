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

        // ROTA POST: Cadastra uma nova partida
        [HttpPost]
        public async Task<ActionResult<Partida>> PostPartida(Partida partida)
        {
            _context.Partidas.Add(partida);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPartidas), new { id = partida.Id }, partida);
        }
    }
}