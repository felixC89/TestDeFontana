using Aplicacion.Implementaciones;
using Aplicacion.Interfaces;
using Dominio.Interfaces;
using Infraestructura.Implementaciones;

namespace ApiVentas.Extensions
{
    public static class InyeccionesExtension
    {
        public static IServiceCollection AddInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConfiguration>(configuration);

            services.AddScoped<IVentasAplicacion, VentasAplicacion>();
            services.AddScoped<IVentasDominio, VentasDominio>();

            return services;
        }
    }
}
