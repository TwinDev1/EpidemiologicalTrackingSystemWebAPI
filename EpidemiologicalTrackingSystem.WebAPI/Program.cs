using EpidemiologicalTrackingSystem.Application.Services;
using EpidemiologicalTrackingSystem.Core.Interfaces;
using EpidemiologicalTrackingSystem.Infrastructure.Data;
using EpidemiologicalTrackingSystem.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Epidemiological API", Version = "v1" });
});

builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(options =>
    options.UseInMemoryDatabase("EpidemiologicalTrackingDB"));

builder.Services.AddScoped<IIndividualRepository, IndividualRepository>();
builder.Services.AddScoped<IndividualService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

app.UseCors("AllowAllOrigins");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Epidemiological API V1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
