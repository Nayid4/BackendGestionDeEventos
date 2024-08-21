using Application.Comun.Behaviors;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Servicios
{
    public static class InyeccionDeDependencias
    {
        public static IServiceCollection AddAplication(this IServiceCollection servicios)
        {
            servicios.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblyContaining<ApplicationAssemblyReference>();
            });

            servicios.AddScoped(
                typeof(IPipelineBehavior<,>),
                typeof(ValidacionBehavior<,>)
            );

            servicios.AddValidatorsFromAssemblyContaining<ApplicationAssemblyReference>();


            return servicios;
        }
    }
}
