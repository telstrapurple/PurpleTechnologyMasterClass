using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TheStillHeron.TestWorkshop.Console.FamilyPlanning;

namespace TheStillHeron.TestWorkshop.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Run().Wait();
        }

        static async Task Run()
        {
            using IHost host = Host.CreateDefaultBuilder()
                .UseEnvironment(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"))
                .Build();

            var config = host.Services.GetRequiredService<IConfiguration>();

            // ex.1
            var engine = new RecommendationEngine(config);
            System.Console.WriteLine(await engine.GetRecommendation());
            // end exercise 1

            // ex.2
            var familyPlanner = FamilyPlanner.Basic();
            System.Console.WriteLine(familyPlanner.DayPlan());

            // end exercise 2

            await host.RunAsync();
        }
    }
}
