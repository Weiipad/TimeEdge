using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePlay.Actions
{
    // <summary>
    // 一种循环Action的条件，要求target不能是循环Action的父节点，否则会形成死循环
    //      <param name="target">在targetAction执行完之前条件不会为假</param>
    // </summary>
    public class LoopWhileActing : LoopAction.LoopCondition
    {
        IAction target;
        public LoopWhileActing(IAction target)
        {
            this.target = target;
        }

        public bool Check()
        {
            return !target.Finished;
        }

        public void Update() { }
    }
}
