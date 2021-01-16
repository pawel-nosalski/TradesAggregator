using System;
using TradesAggregator.Library;

namespace TradesAggregator
{
    class Program
    {
        public static readonly string DefaultRootDataFolder = @"C:\temp\Engineer Code Test";

        static void Main(string[] args)
        {
            var rootDataFolder = DefaultRootDataFolder;
            
            if(args.Length == 0)
            {
                Console.WriteLine($"No data root folder provided as an input parameter, using {DefaultRootDataFolder}");
            }
            else if(args.Length == 1)
            {
                rootDataFolder = args[0];
                Console.WriteLine($"Running trades aggregation with {rootDataFolder} as an input parameter");
            }
            else
            {
                Console.WriteLine("Incorrect number of parameters. Expecting one parameter defining full path to the root data folder !");
            }

            var tradesAggregator = TradesAggregationServiceFactory.Create();
            tradesAggregator.Run(rootDataFolder);            
        }
    }
}
