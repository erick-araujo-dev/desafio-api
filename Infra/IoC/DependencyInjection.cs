using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
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
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IStoreService, StoreService>();


            // Configuração de autenticação
            var authConfig = new AuthConfig();
            authConfig.ConfigureAuthentication(services, configuration);

            // Configuração de Swagger
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JSON Web Token (JWT) is an open standard (RFC 7519) that defines a compact and self-contained way for securely trasmitting informations between parties as a JSON object."
                });

                c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference()
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });

            // Outros serviços
            services.AddControllers();
            services.AddEndpointsApiExplorer();
        }
    }    
}
