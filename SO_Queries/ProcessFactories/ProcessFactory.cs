using System;
using System.Collections.Generic;
using System.Text;
using SO_Queries.Processes;

namespace SO_Queries.ProcessFactories
{
    public class ProcessFactory : IProcessFactory
    {
        private int _minProcessTime = 0;
        private int _maxProcessTime = 0;
        private readonly Random _rnd = new Random();
        private int _minCreateTime = 0;
        private int _maxCreateTime = 0;

        public ProcessFactory(int minProcessTime, int maxProcessTime)
        {
            SetTimeReachInSeconds(minProcessTime, maxProcessTime);
        }

        public ProcessFactory()
        {
        }

        public List<Process> GenerateProcesses(int numberOfProcesses)
        {
            var processes = new List<Process>();
            for (var i = 0; i < numberOfProcesses; i++)
            {
                var processTime = _rnd.Next(_minProcessTime, _maxProcessTime);
                var createTime = _rnd.Next(_minCreateTime, _maxCreateTime);
                processes.Add(new Process(processTime, createTime));
            }
            return processes;
        }

        public void SetTimeReachInSeconds(int min, int max)
        {
            _minProcessTime = min;
            _maxProcessTime = max;
        }

        public void SetCreateTimeReach(int min, int max)
        {
            _minCreateTime = min;
            _maxCreateTime = max;

        }
    }
}
