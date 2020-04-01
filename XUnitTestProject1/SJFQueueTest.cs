using SO_Queries.Processes;
using SO_Queries.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XUnitTestProject1
{
    public class SjfQueryTest
    {
        [Fact]
        public void Test1()
        {
            var query = new Sjf();
            var processes = new List<Process>() { new Process(3), new Process(1), new Process(2), new Process(5) };
            query.AddProcesses(processes);
            
            query.ProcessAll();
            var time = query.GetAverageProcessWaitingTime();

            Assert.Equal(2.5, time);
        }

    }
}
