using System.Collections.Generic;
using SO_Queries.Processes;
using SO_Queries.Queries;
using Xunit;

namespace XUnitTestProject1
{
    public class FifoQueryTest
    {
        [Fact]
        public void Test1()
        {
            var query = new Fifo();
            var processes = new List<Process>() { new Process(3), new Process(1), new Process(2), new Process(5) };
            query.AddProcesses(processes);
            
            query.ProcessAll();
            var time = query.GetAverageProcessWaitingTime();

            Assert.Equal(3.25, time);
        }

        [Fact]
        public void Test2()
        {
            var query = new Fifo();
            var processes = new List<Process>() { new Process(3), new Process(1, 7), new Process(2, 7), new Process(5, 2) };
            query.AddProcesses(processes);

            query.ProcessAll();
            var time = query.GetAverageProcessWaitingTime();

            Assert.Equal(1, time);
        }

    }

}
