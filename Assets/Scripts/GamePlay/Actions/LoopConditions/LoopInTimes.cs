using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePlay.Actions
{
    public class LoopInTimes : LoopAction.LoopCondition
    {
        private uint cur;
        private readonly uint total;

        public LoopInTimes(uint n)
        {
            total = n;
            cur = 0;
        }

        public bool Check()
        {
            return cur < total;
        }

        public void Update()
        {
            cur++;
        }
    }
}
