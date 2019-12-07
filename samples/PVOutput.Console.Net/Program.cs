using System;
using System.Threading.Tasks;
using PVOutput.Net.Objects.Builders;
using PVOutput.Net.Objects.Modules;

namespace PVOutput.Net.Sample
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            //TestGettingData();

            await TestPushingData();

            Console.ReadLine();
        }

        private static async Task TestGettingData()
        {
            var apiKey = Environment.GetEnvironmentVariable("PVOutput-ApiKey");
            var systemIdString = Environment.GetEnvironmentVariable("PVOutput-SystemId");
            var systemId = systemIdString == "" ? 0 : Convert.ToInt32(systemIdString);
            var client = new PVOutputClient(apiKey, systemId);

            // Request output for today
            var outputResponse = await client.Output.GetOutputForDateAsync(DateTime.Today);
            var output = outputResponse.Value;

            Console.WriteLine("Output for past 7 days");
            Console.WriteLine("----------------------");
            OutputDate(output);

            // Request outputs for previous 6 days
            var outputs = await client.Output.GetOutputsForPeriodAsync(DateTime.Today.AddDays(-6), DateTime.Today.AddDays(-1));
            foreach (var dayOutput in outputs.Values)
            {
                OutputDate(dayOutput);
            }

            Console.WriteLine("Insolation");
            Console.WriteLine("----------------------");

            // Request insolation values for 1st of june, for own system - DONATION ONLY
            var insolations = await client.Insolation.GetInsolationForOwnSystem(new DateTime(2019, 6, 1));
            foreach (var insolation in insolations.Values)
            {
                Console.WriteLine($"Insolation on {insolation.Time}, Energy {insolation.Energy} Power {insolation.Power}");
            }
        }

        private static async Task TestPushingData()
        {
            var apiKey = Environment.GetEnvironmentVariable("PVOutput-ApiKey");
            var pushSystemIdString = Environment.GetEnvironmentVariable("PVOutput-PushSystemId");
            var systemId = pushSystemIdString == "" ? 0 : Convert.ToInt32(pushSystemIdString);
            var client = new PVOutputClient(apiKey, systemId);

            Console.WriteLine("Testing pushing data");
            Console.WriteLine("----------------------");

            var builder = new StatusPostBuilder();
            var status = builder.SetDate(DateTime.Now)
                .SetConsumption(null, 560)
                .Build();

            var response = await client.Status.AddStatusAsync(status);
        }

        private static void OutputDate(IOutput output)
        {
            Console.WriteLine($"Output for date {output.Date.ToShortDateString()}, {output.EnergyGenerated} Wh generated");
        }
    }
}
