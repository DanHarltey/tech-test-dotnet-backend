using Microsoft.Extensions.DependencyInjection;

namespace Moonpig.PostOffice.Data
{
    public static class DataServices
    {
        public static void Register(IServiceCollection services) =>
            services.AddScoped<IDbContext, DbContext>();
    }
}
