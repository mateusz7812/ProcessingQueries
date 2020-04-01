using System;
using System.Collections.Generic;
using System.Text;
using SO_Queries.Processes;

namespace SO_Queries
{
    internal interface IProcessFactory
    {
        void SetTimeReachInSeconds(int min, int max);
        void SetCreateTimeReach(int min, int max);
        List<Process> GenerateProcesses(int numberOfProcesses);
    }
}
