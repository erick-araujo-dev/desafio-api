using Microsoft.EntityFrameworkCore;
using SorteOnlineDesafio.Application.Interfaces;
using SorteOnlineDesafio.Application.Services;
using SorteOnlineDesafio.Domain.Interfaces.Commom;
using SorteOnlineDesafio.Domain.Interfaces.Repository;
using SorteOnlineDesafio.Infra.Repositories;
using SorteOnlineDesafio.Security.Configuration;
using SorteOnlineDesafio.Security.Token;

namespace SorteOnlineDesafio.Infra.IoC
{

    public static class DependencyInjection
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            // Configuração do banco de dados
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Repositories 
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddScoped(typeof(IRepositoryBase<,>), typeof(RepositoryBase<,>));
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IPedidoRepository, PedidoRepository>();

            // Services
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUsuarioService, UsuarioService>();


            // Configuração de autenticação
            var authConfig = new AuthConfig();
            authConfig.ConfigureAuthentication(services, configuration);

            // Outros serviços
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }
    }    
}
