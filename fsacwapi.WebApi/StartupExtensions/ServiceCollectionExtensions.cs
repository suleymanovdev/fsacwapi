using FluentValidation;
using FluentValidation.AspNetCore;
using fsacwapi.Core.DTOs.UserDTO.Request;
using fsacwapi.Core.Validations;
using fsacwapi.Infrastructure.Data;
using fsacwapi.Infrastructure.Data.Storage;
using fsacwapi.Infrastructure.Services.JWT;
using fsacwapi.Infrastructure.Services.USER;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace fsacwapi.WebApi.StartupExtensions;

public static class ServiceCollectionExtensions
{
    public static void AddCustomSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "FSACWAPI",
                Version = "v1",
                Description = "FSACWAPI API",
            });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Enter 'Bearer {token}'",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
    }

    public static void AddCustomServices(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        
        services.AddFluentValidationAutoValidation();

        services.AddMemoryCache();
        services.AddScoped<UserService>();
        services.AddScoped<JsonWebTokenService>();
        services.AddScoped<StorageService>();
        services.AddScoped<IValidator<RegisterRequestDTO>, RegisterRequestValidation>();
        services.AddScoped<IValidator<LoginRequestDTO>, LoginRequestValidation>();
    }

    public static void AddCustomDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<DBContext>(options =>
            options.UseNpgsql(connectionString));
    }

    public static void AddCustomAuthentication(this IServiceCollection services, byte[] key)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("Bearer", policy =>
            {
                policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
                policy.RequireAuthenticatedUser();
            });
        });
    }
}