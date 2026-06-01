using FutStats.Models;
using FutStats.Services; // Adicionado para puxar a interface do serviço
using Microsoft.AspNetCore.Mvc;

namespace FutStats.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClassificacaoController : ControllerBase
    {
        private readonly IClassificacaoService _classificacaoService;

        // Injetamos a INTERFACE do serviço, e não a classe diretamente. (Boas práticas!)
        public ClassificacaoController(IClassificacaoService classificacaoService)
        {
            _classificacaoService = classificacaoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Classificacao>>> GetClassificacao()
        {
            // O Controller fica extremamente enxuto. Só pede para o serviço gerar e devolve (200 OK).
            var classificacao = await _classificacaoService.GerarClassificacaoAsync();
            return Ok(classificacao);
        }
    }
}