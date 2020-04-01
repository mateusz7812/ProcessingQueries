using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SO_Queries.Processes;

namespace SO_Queries.Queries
{
    public abstract class QueueStrategy: IQueryStrategy
    {
        public abstract void AddProcesses(List<Process> processes);
        public abstract float GetAverageProcessWaitingTime();
        public abstract void ProcessAll();
        protected abstract void BeforeProcessing();
        protected abstract void RepeatedEverySecondActions();
    }
}
