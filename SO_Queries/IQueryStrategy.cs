using System.Collections.Generic;
using SO_Queries.Processes;

namespace SO_Queries
{
    public interface IQueryStrategy
    {
        void AddProcesses(List<Process> processes);
        float GetAverageProcessWaitingTime();
        void ProcessAll();
    }
}