using FutStats.Models;

namespace FutStats.Services
{
    public interface IClassificacaoService
    {
        Task<IEnumerable<Classificacao>> GerarClassificacaoAsync();
    }
}