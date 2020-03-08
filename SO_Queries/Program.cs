using System;
using System.Collections.Generic;
using SO_Queries.Processes;
using SO_Queries.ProcessFactories;
using SO_Queries.Queries;

namespace SO_Queries
{
    public class Program
    {
        private static readonly ProcessFactory ProcessFactory = new ProcessFactory();
        private static readonly ProcessListPrototype<Process> ProcessListPrototype = new ProcessListPrototype<Process>();
        private static readonly List<IQueryStrategy> QueryStrategies = new List<IQueryStrategy>();
        private static readonly QueryStrategiesFacade QueryService = new QueryStrategiesFacade(ProcessListPrototype, QueryStrategies);

        public static void Main(string[] args)
        {
            QueryStrategies.AddRange(new List<IQueryStrategy> {
                new Fifo(),
                new RoundRobin(10),
                new Sjf(),
                new SjfWithExpropriation(2, 10)
            });

            ProcessFactory.SetTimeReachInSeconds(5, 30);
            ProcessFactory.SetCreateTimeReach(0, 10);
            ProcessListPrototype.SetList(ProcessFactory.GenerateProcesses(50));

            var averageProcessingTimeForQueries = QueryService.GetAverageProcessingTimeForQueries();
            foreach (var (queryName, averageWaitingTime) in averageProcessingTimeForQueries)
            {
                Console.WriteLine(queryName + " query average waiting: " + averageWaitingTime);
            }

            Console.ReadKey();
        }
    }
}
