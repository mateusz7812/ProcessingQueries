using System;

namespace SO_Queries.Processes
{
    public class Process : IProcess, ICloneable
    {
        private readonly int _createTime;
        private int _lastTime;
        private string _state = "creating";

        public Process(int neededProcessingTime, int createTime = 0)
        {
            NeededProcessingTime = neededProcessingTime;
            _createTime = createTime;
        }

        public int ProcessingTime { get; private set; }

        public int WaitingTime { get; private set; }

        public int NeededProcessingTime { get; }

        public bool Done => _state == "done";

        public bool Created => _createTime <= _lastTime;

        public void SetProcessing()
        {
            if (!Done) _state = "processing";
        }

        public void SetWaiting()
        {
            if (!Done) _state = "waiting";
        }

        public void Update(int time)
        {
            switch (_state)
            {
                case "creating" when _createTime == time:
                    _state = "waiting";
                    break;
                case "processing":
                    ProcessingTime += time - _lastTime;
                    break;
                case "waiting":
                    WaitingTime += time - _lastTime;
                    break;
            }

            if (_state != "done")
                if (ProcessingTime >= NeededProcessingTime)
                    _state = "done";

            _lastTime = time;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}