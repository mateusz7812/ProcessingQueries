using System;
using System.Collections.Generic;
using System.Text;

namespace SO_Queries.Queries
{
    public class Sjf :Fifo
    {
        
        public static void SortProcesses(List<IProcess> processes)
        {
            processes.Sort((x, y) => x.NeededProcessingTime - y.NeededProcessingTime);
        }

        protected override void BeforeProcessing()
        {
            base.BeforeProcessing();
            Sjf.SortProcesses(ProcessingProcesses);
        }


    }
}
