using PVOutput.Net;
using System;
using System.Threading.Tasks;

namespace PVOutput.Console.Net
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            System.Console.WriteLine("Hello World!");

            var apiKey = Environment.GetEnvironmentVariable("PVOutput-ApiKey");
            var systemId = Environment.GetEnvironmentVariable("PVOutput-SystemId");

            var client = new PVOutputClient(apiKey, systemId == "" ? 0 : Convert.ToInt32(systemId));

            var output = await client.Output.GetOutputForDateAsync(new DateTime(2018, 9, 4), true);

            System.Console.WriteLine(output.Value.Date);

            var outputs = await client.Output.GetOutputsForPeriodAsync(new DateTime(2018, 9, 1), new DateTime(2018, 9, 7), true);

            System.Console.ReadLine();
        }
    }
}
