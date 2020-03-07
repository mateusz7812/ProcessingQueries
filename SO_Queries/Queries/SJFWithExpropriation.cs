namespace SO_Queries.Queries
{
    public class SjfWithExpropriation: RoundRobin
    {
        private readonly int _sortingInterval;

        public SjfWithExpropriation(int expropriationTime, int sortingInterval) 
            : base(expropriationTime)
        {
            _sortingInterval = sortingInterval;
        }
        

        protected override void BeforeProcessing()
        {
            base.BeforeProcessing();
            Sjf.SortProcesses(ProcessingProcesses);
        }

        protected override void RepeatedEverySecondActions()
        {
            if(Time % _sortingInterval == 0)
            {
                Sjf.SortProcesses(ProcessingProcesses);
            }
            base.RepeatedEverySecondActions();
        }
    }
}
