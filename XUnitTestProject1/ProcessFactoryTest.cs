using SO_Queries;
using SO_Queries.Processes;
using SO_Queries.ProcessFactories;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XUnitTestProject1
{
    public class ProcessFactoryTest
    {
        [Fact]
        public void Test1()
        {
            var processFactory = new ProcessFactory();
            processFactory.SetTimeReachInSeconds(1, 10);
            processFactory.SetCreateTimeReach(1, 20);
            var processes = processFactory.GenerateProcesses(40);
            
            Assert.NotEmpty(processes);
            foreach (var process1 in processes)
            {
                var process = (Process) process1;
                Assert.InRange(process.NeededProcessingTime, 1, 10);
            }
        }

        [Fact]
        public void Test2()
        {
            var processFactory = new ProcessFactory(1, 10);
            processFactory.SetCreateTimeReach(10, 20);
            var processes = processFactory.GenerateProcesses(40);

            Assert.NotEmpty(processes);
            foreach (var process1 in processes)
            {
                var process = (Process)process1;
                process.Update(9);
                Assert.False(process.Created);
                process.Update(20);
                Assert.True(process.Created);
            }
        }
    }
}
