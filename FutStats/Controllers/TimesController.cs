using FutStats.Data;
using FutStats.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FutStats.Controllers
{
    // A tag [ApiController] avisa o C# que esta classe vai lidar com requisições HTTP (JSON).
    [ApiController]
    // A tag [Route] define o caminho da URL. O "[controller]" pega o nome da classe antes da palavra Controller.
    // Portanto, a rota oficial para acessar essa classe será: http://localhost:porta/api/Times
    [Route("api/[controller]")]
    public class TimesController : ControllerBase
    {
        private readonly AppDbContext _context;

        // INJEÇÃO DE DEPENDÊNCIA: Quando a API roda, ela passa o nosso banco de dados na memória para esta variável _context.
        public TimesController(AppDbContext context)
        {
            _context = context;
        }

        // ---------------------------------------------------
        // ROTA GET: Responsável por LISTAR todos os times.
        // ---------------------------------------------------
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Time>>> GetTimes()
        {
            // Vai no banco (_context), entra na tabela Times, e retorna tudo como uma Lista de forma assíncrona.
            return await _context.Times.ToListAsync();
        }

        // ---------------------------------------------------
        // ROTA POST: Responsável por CADASTRAR um novo time.
        // ---------------------------------------------------
        [HttpPost]
        public async Task<ActionResult<Time>> PostTime(Time time)
        {
            // 1. Prepara o novo time para ser inserido no banco
            _context.Times.Add(time);

            // 2. Salva as mudanças de forma assíncrona (não trava a aplicação)
            await _context.SaveChangesAsync();

            // 3. Retorna o Status HTTP 201 (Created) e mostra o time que acabou de ser salvo
            return CreatedAtAction(nameof(GetTimes), new { id = time.Id }, time);
        }
    }
}