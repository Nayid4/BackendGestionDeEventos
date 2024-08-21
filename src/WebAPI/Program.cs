using Application.Servicios;
using Infrastructure.Servicios;
using WebAPI.Extensiones;
using WebAPI.Middlewares;
using WebAPI.Servicios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddPresentation(builder.Configuration)
                .AddInfrastructure(builder.Configuration)
                .AddAplication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

app.UseExceptionHandler("/error");

app.UseHttpsRedirection();

app.UseCors("webLocal");

//app.UseCors("webRemota");

app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
