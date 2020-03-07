using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SO_Queries
{
    public interface IProcess
    {
        void SetProcessing();
        void SetWaiting();
        void Update(int time);
        bool Done { get; }
        int NeededProcessingTime { get; }
        int ProcessingTime { get; }
        int WaitingTime { get; }
        bool Created { get; }
    }
}
