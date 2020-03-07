using SO_Queries.Processes;
using System;
using System.Collections.Generic;
using System.Text;
using SO_Queries.Queries;
using Xunit;

namespace XUnitTestProject1
{
    public class SjfWithExpropriationTest 
    {
        [Fact]
        public void Test1()
        {
            var query = new SjfWithExpropriation(3, 30);
            var processes = new List<Process>() { new Process(3), new Process(8), new Process(2), new Process(5) };
            query.AddProcesses(processes);
            
            query.ProcessAll();
            var time = query.GetAverageProcessWaitingTime();

            Assert.Equal(5, time);
        } 
    }
}
