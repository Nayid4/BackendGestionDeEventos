using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Servicios
{
    public static class InyeccionDeDependencias
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection servicios, IConfiguration configuracion)
        {
            servicios.AgregarPersistencias(configuracion);
            return servicios;
        }

        public static IServiceCollection AgregarPersistencias(this IServiceCollection servicios, IConfiguration configuracion)
        {
            servicios.AddDbContext<AplicacionContextoDb>(options =>
                options.UseSqlServer(configuracion.GetConnectionString("Database")));

            servicios.AddScoped<IAplicacionContextoDb>(sp =>
                sp.GetRequiredService<AplicacionContextoDb>());

            servicios.AddScoped<IUnitOfWork>(sp =>
                sp.GetRequiredService<AplicacionContextoDb>());

            return servicios;
        }
}
