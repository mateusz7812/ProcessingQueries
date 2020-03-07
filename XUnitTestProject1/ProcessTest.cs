using SO_Queries.Processes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestProject1
{
    public class ProcessTest
    {
        [Fact]
        public void TestPause()
        {
            var process = new Process(5);

            process.SetProcessing();
            process.Update(1);
            process.SetWaiting();
            process.Update(2);
            process.SetProcessing();
            process.Update(3);
            process.SetWaiting();
            process.Update(4);
            process.SetProcessing();

            process.Update(7);
            Assert.True(process.Done);
            Assert.Equal(2, process.WaitingTime);
            Assert.Equal(5, process.ProcessingTime);
        }

        [Fact]
        public void Test1()
        {
            var process = new Process(5);
            var process2 = new Process(5);

            process.Update(0);
            process2.Update(0);

            process.SetProcessing();
            process.Update(5);
            Assert.True(process.Done);
            process2.Update(5);
            process2.SetProcessing();
            process2.Update(10);
            Assert.True(process2.Done);
            Assert.Equal(0, process.WaitingTime);
            Assert.Equal(5, process.ProcessingTime);
            Assert.Equal(5, process2.WaitingTime);
            Assert.Equal(5, process2.ProcessingTime);
        }

        [Fact]
        public void TestExpropriation()
        {
            var process1 = new Process(11);
            var process2 = new Process(5);

            process1.Update(0);
            process2.Update(0);

            process1.SetProcessing();
            process1.Update(3);
            process1.SetWaiting();

            process2.Update(3);
            process2.SetProcessing();
            process2.Update(6);
            process2.SetWaiting();

            process1.Update(6);
            process1.SetProcessing();
            process1.Update(9);
            process1.SetWaiting();

            process2.Update(9);
            process2.SetProcessing();
            process2.Update(12);
            process2.SetWaiting();

            process1.Update(12);
            process1.SetProcessing();
            process1.Update(15);
            process1.SetWaiting();

            process1.Update(15);
            process1.SetProcessing();
            process1.Update(18);
            process1.SetWaiting();


            Assert.True(process1.Done);
            Assert.True(process2.Done);
            Assert.Equal(6, process1.WaitingTime);
            Assert.Equal(6, process2.WaitingTime);
        }

        [Fact]
        public void TestExpropriationWithDifferentCreateTimes()
        {

            var process1 = new Process(3);
            var process2 = new Process(2, 2);
            var process3 = new Process(3, 2);

            process1.Update(0);
            process2.Update(0);
            process3.Update(0);
            process1.SetProcessing();

            process1.Update(2);
            process2.Update(2);
            process3.Update(2);

            process1.Update(3);
            process2.Update(3);
            process3.Update(3);
            process1.SetWaiting();
            process2.SetProcessing();

            process1.Update(5);
            process2.Update(5);
            process3.Update(5);
            process2.SetWaiting();
            process3.SetProcessing();

            process1.Update(8);
            process2.Update(8);
            process3.Update(8);
            process3.SetWaiting();

            Assert.Equal(0, process1.WaitingTime);
            Assert.Equal(1, process2.WaitingTime);
            Assert.Equal(3, process3.WaitingTime);

        }
    }
}
