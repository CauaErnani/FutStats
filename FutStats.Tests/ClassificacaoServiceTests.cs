using FutStats.Data;
using FutStats.Models;
using FutStats.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace FutStats.Tests
{
    public class ClassificacaoServiceTests
    {
        // O [Fact] avisa o Visual Studio que este método é um teste automatizado
        [Fact]
        public async Task GerarClassificacaoAsync_DeveCalcularVitoriaCorretamente()
        {
            // 1. ARRANGE (Preparação): Criamos um banco de dados falso só para este teste
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Banco novo a cada teste
                .Options;

            using var context = new AppDbContext(options);

            // Criamos dois times
            var timeCasa = new Time { Id = 1, Nome = "São Paulo" };
            var timeFora = new Time { Id = 2, Nome = "Palmeiras" };
            context.Times.AddRange(timeCasa, timeFora);

            // Criamos uma partida onde o Time da Casa ganhou de 2x0
            var partida = new Partida
            {
                Id = 1,
                IdTimeCasa = 1,
                IdTimeFora = 2,
                PlacarTimeCasa = 2,
                PlacarTimeFora = 0
            };
            context.Partidas.Add(partida);
            await context.SaveChangesAsync();

            // Instanciamos o seu serviço passando o banco falso
            var service = new ClassificacaoService(context);

            // 2. ACT (Ação): Mandamos o serviço gerar a tabela
            var tabela = await service.GerarClassificacaoAsync();
            var tabelaList = tabela.ToList();

            // 3. ASSERT (Verificação): Conferimos se a sua matemática funcionou
            var primeiroColocado = tabelaList[0];
            var segundoColocado = tabelaList[1];

            // Verificações do Primeiro Colocado (São Paulo)
            Assert.Equal("São Paulo", primeiroColocado.NomeTime);
            Assert.Equal(3, primeiroColocado.Pontos);
            Assert.Equal(1, primeiroColocado.Vitorias);
            Assert.Equal(2, primeiroColocado.SaldoGols);

            // Verificações do Segundo Colocado (Palmeiras)
            Assert.Equal("Palmeiras", segundoColocado.NomeTime);
            Assert.Equal(0, segundoColocado.Pontos);
            Assert.Equal(1, segundoColocado.Derrotas);
            Assert.Equal(-2, segundoColocado.SaldoGols);
        }
    }
}