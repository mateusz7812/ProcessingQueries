using System;
using System.Collections.Generic;
using System.Linq;
using SO_Queries.Processes;
using SO_Queries.ProcessFactories;
using SO_Queries.Queries;

namespace SO_Queries
{
    public class Program
    {
        private static readonly ProcessFactory ProcessFactory = new ProcessFactory();
        private static readonly ReadOnlyList<Process> ProcessesList = new ReadOnlyList<Process>();
        private static readonly List<IQueryStrategy> QueryStrategies = new List<IQueryStrategy>();
        private static readonly QueryStrategyService QueryService = new QueryStrategyService(ProcessesList, QueryStrategies);

        private class ReadOnlyList<T> where T : ICloneable
        {
            private List<T> _list;

            public ReadOnlyList() { }

            public void SetList(List<T> list)
            {
                _list = list;
            }

            public List<T> Clone()
            {
                if (_list == null)
                {
                    throw new NullReferenceException();
                }

                return _list.Select(item => (T)item.Clone()).ToList();
            }

        }

        private class QueryStrategyService
        {
            private ReadOnlyList<Process> _list;
            private List<IQueryStrategy> _queryStrategies;

            public QueryStrategyService() { }

            public QueryStrategyService(ReadOnlyList<Process> list, List<IQueryStrategy> queryStrategies)
            {
                SetProcesses(list);
                SetQueryStrategies(queryStrategies);
            }

            public void SetQueryStrategies(List<IQueryStrategy> queryStrategies)
            {
                _queryStrategies = queryStrategies;
            }

            public void SetProcesses(ReadOnlyList<Process> list)
            {
                _list = list;
            }

            public Dictionary<string, float> GetAverageProcessingTimeForQueries()
            {
                var dict = new Dictionary<string, float>();
                _queryStrategies.ForEach((strategy =>
                {
                    strategy.AddProcesses(_list.Clone());
                    strategy.ProcessAll();
                    var averageProcessWaitingTime = strategy.GetAverageProcessWaitingTime();
                    dict.Add(strategy.GetType().Name, averageProcessWaitingTime);
                }));
                return dict;
            }
        }

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
            ProcessesList.SetList(ProcessFactory.GenerateProcesses(50));

            var averageProcessingTimeForQueries = QueryService.GetAverageProcessingTimeForQueries();
            foreach (var (queryName, averageWaitingTime) in averageProcessingTimeForQueries)
            {
                Console.WriteLine(queryName + " query average waiting: " + averageWaitingTime);
            }

            Console.ReadKey();
        }
    }
}
