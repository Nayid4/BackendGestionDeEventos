using Application.Datos;
using Domain.Primitivos;
using Domain.Usuarios;
using Infrastructure.Persistencia;
using Infrastructure.Persistencia.Repositorios;
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
            servicios.AddDbContext<AplicacionDeContextoDB>(options =>
                options.UseSqlServer(configuracion.GetConnectionString("Database")));

            servicios.AddScoped<IAplicacionDeContextoDB>(sp =>
                sp.GetRequiredService<AplicacionDeContextoDB>());

            servicios.AddScoped<IUnitOfWork>(sp =>
                sp.GetRequiredService<AplicacionDeContextoDB>());

            servicios.AddScoped<IRepositorioUsuario, RepositorioUsuario>();

            return servicios;
        }
    }
}
