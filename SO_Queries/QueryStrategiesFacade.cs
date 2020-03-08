using System.Collections.Generic;
using SO_Queries.Processes;

namespace SO_Queries
{
    public class QueryStrategiesFacade
    {
            private ProcessListPrototype<Process> _list;
            private List<IQueryStrategy> _queryStrategies;

            public QueryStrategiesFacade() { }

            public QueryStrategiesFacade(ProcessListPrototype<Process> list, List<IQueryStrategy> queryStrategies)
            {
                SetProcesses(list);
                SetQueryStrategies(queryStrategies);
            }

            public void SetQueryStrategies(List<IQueryStrategy> queryStrategies)
            {
                _queryStrategies = queryStrategies;
            }

            public void SetProcesses(ProcessListPrototype<Process> list)
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
    
}