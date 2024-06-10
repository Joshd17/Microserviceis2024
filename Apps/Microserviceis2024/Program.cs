
using MassTransit;
using Microserviceis2024.Middleware;
using Microserviceis2024.Signalr;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Values;

namespace Microserviceis2024;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        //builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        //builder.Services.AddEndpointsApiExplorer();
        //builder.Services.AddSwaggerGen();
        builder.Services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("host.docker.internal", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
            });
        });
        builder.Services.AddHostedService<BackgroundListener>();

        builder.Services.AddHostedService<BusService>();
        //builder.Configuration.AddJsonFile(Path.Combine("configuration",
        //    "ocelot.json"));

        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("ocelot.json")
            .Build();
        
        builder.Services.AddOcelot(configuration);
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        //if (app.Environment.IsDevelopment())
        //{
        //    app.UseSwagger();
        //    app.UseSwaggerUI();
        //}
        //app.UseHttpsRedirection();
        await app.UseOcelot();
        //app.UseAuthorization();
        app.UseMiddleware<HeaderMiddleware>();

       // app.MapControllers();

        await app.RunAsync();
    }
}
