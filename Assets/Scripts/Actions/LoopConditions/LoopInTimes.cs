namespace GamePlay.Actions
{
    public class LoopInTimes : LoopAction.LoopCondition
    {
        private int cur;
        private readonly int total;

        public LoopInTimes(int n)
        {
            total = n;
            cur = 0;
        }

        public bool Check()
        {
            if (total < 0)
                return true;
            return cur < total;
        }

        public void Update()
        {
            cur++;
        }

        public LoopAction.LoopCondition Duplicate()
        {
            return new LoopInTimes(total);
        }
    }
}
