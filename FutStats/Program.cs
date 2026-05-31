using FutStats.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Adiciona o nosso banco de dados na memória aos serviços da API
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("BancoFutStats"));

builder.Services.AddControllers();

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