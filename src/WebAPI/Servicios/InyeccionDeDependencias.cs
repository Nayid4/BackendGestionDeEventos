using WebAPI.Middlewares;

namespace WebAPI.Servicios
{
    public static class InyeccionDeDependencias
    {
        public static IServiceCollection AddPresentation(this IServiceCollection servicios, IConfiguration configuracion)
        {
            servicios.AddCors(options =>
            {

                servicios.AddControllers();
                servicios.AddEndpointsApiExplorer();
                servicios.AddSwaggerGen();
                servicios.AddTransient<GlobalExceptionHandlingMiddleware>();

                options.AddPolicy("webLocal", policyBuilder =>
                {
                    policyBuilder.WithOrigins("http://localhost:4200");
                    policyBuilder.AllowAnyHeader();
                    policyBuilder.AllowAnyMethod();
                    policyBuilder.AllowCredentials();
                });

                options.AddPolicy("webRemota", policyBuilder =>
                {
                    policyBuilder.WithOrigins("https://frontend-gestion-de-eventos.vercel.app/")
                                 .AllowAnyHeader()
                                 .AllowAnyMethod()
                                 .AllowCredentials();
                });
            });

            return servicios;
        }
    }
}
