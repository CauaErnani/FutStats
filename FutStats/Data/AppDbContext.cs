using FutStats.Models;
using Microsoft.EntityFrameworkCore;

namespace FutStats.Data
{
    // A nossa classe herda (:) de DbContext, que traz toda a inteligência do Entity Framework
    public class AppDbContext : DbContext
    {
        // O construtor recebe as configurações (como o tipo do banco) e repassa para a classe base
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Os DbSets representam as tabelas no banco de dados. 
        // Estamos dizendo que teremos uma tabela de Times e uma de Partidas.
        public DbSet<Time> Times { get; set; }
        public DbSet<Partida> Partidas { get; set; }
    }
}