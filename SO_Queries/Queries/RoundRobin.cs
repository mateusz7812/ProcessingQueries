using System;
using System.Collections.Generic;
using System.Text;

namespace SO_Queries.Queries
{
    public class RoundRobin: Fifo
    {

        private readonly int _maxExpropriationTime;

        private int _expropriationTime = 0;
        private IProcess _processingProcess;

        public RoundRobin(int maxExpropriationTime)
        {
            this._maxExpropriationTime = maxExpropriationTime;
        }

        public new void ProcessAll()
        {
            
            BeforeProcessing();

            _processingProcess = TakeNext();
            while (true)
            {
                RepeatedEverySecondActions();

                if (_processingProcess.Done)
                {
                    _expropriationTime = 0;
                    MoveProcessToEnded(_processingProcess);
                    if (ProcessingProcesses.Count <= 0) break;
                    _processingProcess = TakeNext();
                    ;
                }
                else
                {
                    if (IsExpropriationEnd(_expropriationTime))
                    {
                        _expropriationTime = 0;
                        _processingProcess.SetWaiting();
                        MoveToEnd(_processingProcess);
                        _processingProcess = TakeNext();
                        continue;
                    }
                }
                _expropriationTime++;
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

        private bool IsExpropriationEnd(int i)
        {
            return i == _maxExpropriationTime;
        }

        private void MoveToEnd(IProcess process)
        {
            ProcessingProcesses.Remove(process);
            ProcessingProcesses.Add(process);
        }

        private IProcess TakeNext()
        {
            var process = ProcessingProcesses[0];
            process.Update(Time);
            process.SetProcessing();
            return process;
        }
        
    }
}
