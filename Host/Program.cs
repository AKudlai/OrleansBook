using Orleans;
using Orleans.Hosting;
using GrainClasses;
using Microsoft.Extensions.Hosting;

namespace Host;

internal class Program
{
    static async Task Main()
    {
        var host = new HostBuilder().UseOrleans(builder =>
            builder.ConfigureApplicationParts(
                parts => parts.AddApplicationPart(typeof(RobotGrain).Assembly).WithReferences()).UseLocalhostClustering()

        ).Build();

        await host.StartAsync();

        Console.WriteLine("Press enter to stop the Silo…");
        Console.ReadLine();

        await host.StopAsync();
    }
}
