using System.Reflection;
using CaminhoLivre.Infrastructure.Persistence;
using CaminhoLivre.Infrastructure.Repositories.Catalogo;
using CaminhoLivre.Modulo.Catalogo.Application.Interfaces;
using CaminhoLivre.Modulo.Catalogo.Application.Services;
using CaminhoLivre.Modulo.Catalogo.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = "CaminhoLivre ERP", Version = "v1" });

    // 1. Caminho para o XML da WebApi (onde ficam as Controllers)
    var xmlWebApi = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var caminhoWebApi = Path.Combine(AppContext.BaseDirectory, xmlWebApi);
    if (File.Exists(caminhoWebApi))
    {
        options.IncludeXmlComments(caminhoWebApi);
    }

    // 2. Caminho para o XML da Application (onde ficam os seus DTOs)
    // Substitua pelo nome exato do assembly/projeto da sua Application
    var xmlApplication = "CaminhoLivre.Modulo.Catalogo.Application.xml";
    var caminhoApplication = Path.Combine(AppContext.BaseDirectory, xmlApplication);
    if (File.Exists(caminhoApplication))
    {
        options.IncludeXmlComments(caminhoApplication);
    }
});


//CONFIGURAÇÃO DO BANCO DE DADOS
var connectionString = builder.Configuration.GetConnectionString("PostgresConnection");
builder.Services.AddDbContext<CaminhoLivreDbContext>(options =>
    options.UseNpgsql(connectionString));

//INJEÇÃO DOS REPOSITÓRIOS 
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();

//INJEÇÃO DOS SERVIÇOS (Application)
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<IProdutoService, ProdutoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
