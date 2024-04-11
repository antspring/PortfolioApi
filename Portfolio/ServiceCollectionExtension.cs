using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Portfolio;

public static class ServiceCollectionExtension
{
    public static void AddPortfolioServices(this IServiceCollection services)
    {
        services.AddDbContext<PortfolioDbContext>(options =>
            options.UseNpgsql(Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING")));
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
                ValidIssuer = Environment.GetEnvironmentVariable("ISSUER"),
                ValidAudience = Environment.GetEnvironmentVariable("AUDIENCE"),
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECURITY_KEY") ?? string.Empty))
            };
        });
    }
    
    public static void AddDependencies(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<AuthenticationService>();
    }
}