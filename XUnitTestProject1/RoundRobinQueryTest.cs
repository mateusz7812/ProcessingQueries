using SO_Queries.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using SO_Queries.Processes;

namespace XUnitTestProject1
{
    public class RoundRobinQueryTest
    {
        [Fact]
        public void TestBasic()
        {

            var query = new RoundRobin(3);
            var processes = new List<Process>() { new Process(3), new Process(8), new Process(2), new Process(5) };
            query.AddProcesses(processes);

            query.ProcessAll();
            var time = query.GetAverageProcessWaitingTime();

            Assert.Equal(6.75, time);
        }

        [Fact]
        public void TestWithDifferentCreateTimes()
        {

            var query = new RoundRobin(3);
            var processes = new List<Process>() { new Process(3), new Process(2, 2), new Process(6, 2), new Process(5, 2), new Process(5, 12), new Process(2, 12), new Process(3, 20) };
            query.AddProcesses(processes);

            query.ProcessAll();
            var time = query.GetAverageProcessWaitingTime();

            Assert.Equal(4.57, Math.Round(time, 2));
        }
    }
}
