using Microsoft.Extensions.Configuration;
using ProducerIrongridC2System.Models;
using ProducerIrongridC2System.Services;

class Program
{
    static async Task Main(string[] args)
    {
        var configuration = new ConfigurationBuilder().
            SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();
        var loader = new Loader();
        var baseDir = AppContext.BaseDirectory;

        var path = Path.Combine(baseDir, "Data", "field_reports.json");
        var allData = loader.LoadFromJson(path);
        
        var bootstrapServers = configuration["Kafka:BootstrapServers"]?? "localhost:9092";
        var uavTopic = configuration["Kafka:Topics:UAVs"]?? "uavs";
        var perimeterSensorsTopic = configuration["Kafka:Topics:PerimeterSensors"]?? "perimeter_sensors";
        
        var UAVs = loader.CheckType(allData, "UAV");
        Console.WriteLine(UAVs.Count);
        var PerimeterSensors = loader.CheckType(allData, "PerimeterSensor");
        Console.WriteLine(PerimeterSensors.Count);

        var producer = new ProducerService(bootstrapServers);
        await producer.EnshurTopicExists(uavTopic);
        await producer.EnshurTopicExists(perimeterSensorsTopic);
        foreach (AssetReport uav in UAVs)
        {
            await producer.SendToKafka(uavTopic, uav);
            Console.WriteLine(uav);
        }
        foreach (AssetReport perimeterSensor in PerimeterSensors)
        {
            await producer.SendToKafka(perimeterSensorsTopic, perimeterSensor);
            Console.WriteLine(perimeterSensor);
        }
    }
}