using Microsoft.Extensions.DependencyInjection;
using Moonpig.PostOffice.Core.Interactors;
using Moonpig.PostOffice.Core.Interfaces;

namespace Moonpig.PostOffice.Core
{
    public static class CoreServices
    {
        public static void Register(IServiceCollection services) =>
            services.AddScoped<IDespatchDateInteractor, DespatchDateInteractor>();
    }
}
