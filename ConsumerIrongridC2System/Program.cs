using Confluent.Kafka;
using ConsumerIrongridC2System.Data;
using ConsumerIrongridC2System.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

class Program
{
    static async Task Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();
        var services = new ServiceCollection();
        var connectionString = configuration.GetConnectionString("IrongridC2ConnectionString");
        services.AddDbContext<ConsumerDbContext>(options =>
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
        services.AddScoped<ConsumerService>();
        var provider = services.BuildServiceProvider();
        using (var scope = provider.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<ConsumerDbContext>();
            db.Database.EnsureCreated();
        }
        var bootstrapServers = configuration["Kafka:BootstrapServers"] ?? "localhost:9092";
        var groupId = configuration["Kafka:GroupId"] ?? "irongrid-c2-system"+Guid.NewGuid();

        var config = new ConsumerConfig
        {
            BootstrapServers = bootstrapServers,
            GroupId = groupId,
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = false
        };
        var consumer = new ConsumerBuilder<Ignore, string>(config).Build();

        consumer.Subscribe(configuration["Kafka:Topics:UAVs"]);
        try
        {
            while (true)
            {
                var result = consumer.Consume(TimeSpan.FromSeconds(10));
                if (result == null)
                {
                    Console.WriteLine("done");
                    break;
                }
                using (var scop = provider.CreateScope())
                {
                    var prossec = scop.ServiceProvider.GetRequiredService<ConsumerService>();
                    await prossec.ConsunTypeUAV(result.Message.Value);
                    Console.WriteLine("The change was made successfully.");
                    consumer.Commit();
                }
            }
        }
        catch (OperationCanceledException) { Console.WriteLine("fildAA"); }



        await Task.Delay(10);

        consumer.Unsubscribe();
        consumer.Subscribe(configuration["Kafka:Topics:PerimeterSensors"]);
        try
        {
            while (true)
            {
                var result = consumer.Consume(TimeSpan.FromSeconds(10));
                if (result == null)
                {
                    Console.WriteLine("done");
                    break;
                }
                using (var scop = provider.CreateScope())
                {
                    var prossec = scop.ServiceProvider.GetRequiredService<ConsumerService>();
                    await prossec.ConsunTypePerimeterSensors(result.Message.Value);
                    Console.WriteLine("The change was made successfully.");
                    consumer.Commit();
                }
            }
        }
        catch (OperationCanceledException) { Console.WriteLine("fildBB"); }
        finally { consumer.Close(); }
    }
}