using System;
using System.Collections.Generic;
using System.Linq;
using SO_Queries.Processes;

namespace SO_Queries.Queries
{
    public class Fifo : QueryStrategy
    {
        protected readonly List<IProcess> CreatingProcesses = new List<IProcess>();
        protected readonly List<IProcess> ProcessingProcesses = new List<IProcess>();
        protected readonly List<IProcess> EndedProcesses = new List<IProcess>();
        protected int Time = 0;

        public override void AddProcesses(List<Process> processes)
        {
            CreatingProcesses.AddRange(processes);
        }
        
        public override float GetAverageProcessWaitingTime()
        {
            var allWaitingTime = EndedProcesses.Aggregate<IProcess, float>(0, (current, process) => current + process.WaitingTime);
            return allWaitingTime / EndedProcesses.Count;
        }

        public override void ProcessAll()
        {

            BeforeProcessing();

            var process = PickNextProcess();
            process?.SetProcessing();
            while (true)
            {
                RepeatedEverySecondActions();

                if (process == null)
                {
                    process = PickNextProcess();
                    process?.SetProcessing();
                    Time++;
                    continue;
                }


                if (process.Done)
                {
                    MoveProcessToEnded(process);

                    if (ProcessingProcesses.Count == 0 && CreatingProcesses.Count == 0) break;

                    process = PickNextProcess();
                    process?.Update(Time);
                    process?.SetProcessing();
                    continue;
                }

                Time++;

            }
        }

        protected override void RepeatedEverySecondActions()
        {
            UpdateProcesses(Time);
            MoveCreatedProcessesToProcessing();
        }

        protected override void BeforeProcessing()
        {
            UpdateProcesses(Time);
            MoveCreatedProcessesToProcessing();
        }

        protected void UpdateProcesses(int time)
        {
            CreatingProcesses.ForEach((IProcess p) => p.Update(time));
            ProcessingProcesses.ForEach((IProcess p) => p.Update(time));
        }

        protected void MoveCreatedProcessesToProcessing()
        {
            var length = CreatingProcesses.Count;
            for (var i = 0; i < length;)
            {
                var p = CreatingProcesses[i];
                if (p.Created)
                {
                    MoveToProcessing(p);
                    length--;
                }
                else i++;
                
            }
        }

        private void MoveToProcessing(IProcess process)
        {
            CreatingProcesses.Remove(process);
            ProcessingProcesses.Add(process);
        }

        protected void MoveProcessToEnded(IProcess process)
        {
            ProcessingProcesses.Remove(process);
            EndedProcesses.Add(process);
        }

        protected IProcess PickNextProcess()
        {
            return ProcessingProcesses.Count != 0 ? ProcessingProcesses[0] : null;
        }
        
    }
}
