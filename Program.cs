using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SorteOnlineDesafio.Domain.Entities; 
using SorteOnlineDesafio.Domain.Interfaces.Commom;
using SorteOnlineDesafio.Domain.Interfaces.Repository;
using SorteOnlineDesafio.Infra.IoC;
using SorteOnlineDesafio.Infra.Repositories;
using SorteOnlineDesafio.Security.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Configuração dos serviços
DependencyInjection.Configure(builder.Services, builder.Configuration);

var app = builder.Build();

// Configuração do pipeline de requisição HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

// Mapeamento dos controllers
app.MapControllers();

app.Run();
