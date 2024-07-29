using System.Reflection;
using System.Text;
using Domain;
using Domain.Command.App;
using Domain.Command.Auth;
using Infrastructure;
using Infrastructure.Repository;
using Microsoft.OpenApi.Models;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Application;

public static class ServiceExtensions {
    
    /**
     * Configuracion de Swagger
     */
    public static void InitSwagger(this IServiceCollection services) {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>  {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "SoftDocApi", Version = "v1"});
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
                In = ParameterLocation.Header,
                Description = "Ingresar token valido",
                Name = "Autorización por token",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
    
            options.AddSecurityRequirement(new OpenApiSecurityRequirement { {
                    new OpenApiSecurityScheme {
                        Reference = new OpenApiReference {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
        
        services.AddCors(options => {
            options.AddDefaultPolicy(builder => {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            });
        });
    }

    /**
     * Configuracion a base de datos postgres
     */
    public static void InitDatabase(this IServiceCollection services, IConfiguration configuration) {
        AppContext.SetSwitch("Npgsql.EnableStoredProcedureCompatMode", true);
        var connectionParam = configuration.GetConnectionString("PostgreSQL");
        services.Add(new ServiceDescriptor(typeof(DatabaseContext), new DatabaseContext(connectionParam)));
    }
    
    /**
     * Configurar mediator
     */
    public static void InitMediator(this IServiceCollection services) {
        services.AddMediatR(typeof(CreateUserCommand).GetTypeInfo().Assembly);
        services.AddMediatR(typeof(GetUserPaginatedCommand).GetTypeInfo().Assembly);
        services.AddMediatR(typeof(LoginCommand).GetTypeInfo().Assembly);
        services.AddMediatR(typeof(SyncUserDetailCommand).GetTypeInfo().Assembly);
        services.AddMediatR(Assembly.GetExecutingAssembly());
    }

    /**
     * Inicializa los repositorios
     */
    public static void InitRepositories(this IServiceCollection service, IConfiguration configuration) {
        service.AddSingleton(new UserRepository());
        service.AddSingleton(new AuthRepository());
        service.AddSingleton(new AppRepository());
        
        service.AddSingleton(new TokenService(
            configuration["Jwt:Issuer"],
            configuration["Jwt:Key"]
        ));
    }
    
    public static void InitMapper(this IServiceCollection service) {
        service.AddAutoMapper(typeof(MappingRequestProfile));
    }

    public static void InitToken(this IServiceCollection service, IConfiguration configuration) {
        service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                };
            });
    }
    
}