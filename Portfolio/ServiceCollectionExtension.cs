using System.Text;
using DataAccess;
using DataAccess.Repositories.Implementations;
using Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Services.Services.Tokens;

namespace Portfolio;

public static class ServiceCollectionExtension
{
    public static void AddPortfolioServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContext<PortfolioDbContext>(options =>
            options.UseNpgsql(configuration["DATABASE_CONNECTION_STRING"]));
        services.AddControllers();
        services.AddAuthorization();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = configuration["ISSUER"],
                ValidAudience = configuration["AUDIENCE"],
                IssuerSigningKey =
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["SECURITY_KEY"] ?? string.Empty))
            };
        });
    }

    public static void AddDependencies(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<AuthenticationService>();
        services.AddScoped<TokenService>();
        services.AddSingleton<TokenGenerator>();
        services.AddScoped<UserRepository>();
        services.AddScoped<RefreshSessionRepository>();
        services.AddTransient<ImagesService>();
        services.AddTransient<UserService>();
    }
}