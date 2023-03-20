using GrainInterfaces;
using Orleans;

namespace Client;

internal class Program
{
    private static async Task Main()
    {
        var client = new ClientBuilder()
            .UseLocalhostClustering()
            .Build();

        await using (client)
        {
            await client.Connect();

            while (true)
            {
                Console.WriteLine("Please enter a robot name:");
                var grainId = Console.ReadLine();

                var grain = client.GetGrain<IRobotGrain>(grainId);

                Console.WriteLine("Please enter an instruction…");
                var instruction = Console.ReadLine();
                if (!string.IsNullOrEmpty(instruction))
                {
                    await grain.AddInstruction(instruction);
                }

                var count = await grain.GetInstructionCount();
                Console.WriteLine($"{grainId} has {count} instruction(s)");
            }
        }
    }
}