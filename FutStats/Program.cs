using FutStats.Data;
using Microsoft.EntityFrameworkCore;
using FluentValidation; // Adicionado para o FluentValidation
using FutStats.Validators; // Adicionado para enxergar as suas regras
using FutStats.Services;

var builder = WebApplication.CreateBuilder(args);

// Adiciona o nosso banco de dados na memória aos serviços da API
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("BancoFutStats"));

builder.Services.AddControllers();

// INJEÇÃO DE DEPENDÊNCIA: Registra todas as regras de negócio de uma vez só!
builder.Services.AddValidatorsFromAssemblyContaining<PartidaValidator>();

// Injeção do Serviço de Classificação (AddScoped cria uma instância nova por requisição)
builder.Services.AddScoped<IClassificacaoService, ClassificacaoService>();

// Adiciona as ferramentas do Swagger UI (Interface Visual)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configura o ambiente
if (app.Environment.IsDevelopment())
{
    // Ativa a página do Swagger no navegador
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();