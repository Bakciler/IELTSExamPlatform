using IELTSExamPlatform.CORE.Entities;
using IELTSExamPlatform.DAL.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IELTSExamPlatform.DAL
{
    public static class ServiceRegistration
    {
        public static void AddPostgreSQLServices(this IServiceCollection services, IConfiguration configuration)
        {
            // DbContext əlavə olunur
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("PostgreSQL")));

            // Identity servisləri əlavə olunur
            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
        }
    }
}
