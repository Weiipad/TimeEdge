using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine.Animations;
using UnityEngine;
using System.CodeDom;

namespace GamePlay.Actions
{
    public abstract class IAction
    {
        public abstract bool Finished { get; }

        public abstract void Act();

        public abstract IAction Duplicate();
    }

    public class Condition : IAction
    {
        public delegate bool TransmitCondition();
        private readonly IAction subAction;

        public override bool Finished { get => subAction.Finished;  }

        private TransmitCondition cond;

        public Condition(IAction action, TransmitCondition condition)
        {
            cond = condition;
            subAction = action;
        }

        public override void Act()
        {
            if (cond.Invoke())
            {
                subAction.Act();
            }
        }

        public override IAction Duplicate()
        {
            return new Condition(subAction.Duplicate(), cond);
        }
    }

    public class Branch : IAction
    {
        private int ptr = 0;
        protected List<IAction> actions = null;

        protected bool isFinish = false;
        public override bool Finished { get => isFinish; }

        public void AddSubAction(IAction action)
        {
            if (actions == null) actions = new List<IAction>();
            actions.Add(action);
        }

        public override void Act()
        {
            if (actions != null && ptr < actions.Count)
            {
                var ac = actions[ptr];
                ac.Act();
                if (ac.Finished)
                {
                    ptr++;
                }
                return;
            }

            isFinish = true;
        }

        public override IAction Duplicate()
        {
            Branch branch = new Branch();
            branch.isFinish = false;
            if (actions != null)
            {
                branch.actions = new List<IAction>();
                foreach (var ac in actions)
                {
                    branch.actions.Add(ac.Duplicate());
                }
            }
            return branch;
        }
    }

    public class Parallel : IAction
    {
        private bool isFinish;
        public override bool Finished => isFinish;

        private List<IAction> actions = new List<IAction>();

        public void AddSubAction(IAction action)
        {
            actions.Add(action);
        }

        public override void Act()
        {
            bool allFinished = true;
            foreach (var action in actions)
            {
                if (!action.Finished)
                {
                    action.Act();
                    allFinished = false;
                }
            }

            if (allFinished)
            {
                isFinish = true;
            }
        }

        public override IAction Duplicate()
        {
            Parallel p = new Parallel();
            p.isFinish = false;
            if (actions != null)
            {
                p.actions = new List<IAction>();
                foreach (var ac in actions)
                {
                    p.actions.Add(ac.Duplicate());
                }
            }
            return p;
        }
    }

    public class LoopAction: IAction
    {
        public interface LoopCondition
        {
            bool Check();
            void Update();

            LoopCondition Duplicate();
        }

        private bool isFinish;
        private List<IAction> actions;
        private int ptr = 0;
        public override bool Finished => isFinish;

        private LoopCondition loopCondition;

        public LoopAction(LoopCondition condition)
        {
            loopCondition = condition;
            actions = new List<IAction>();
            isFinish = false;
        }

        public void PushAction(IAction action)
        {
            actions.Add(action);
        }

        public override void Act()
        {
            if (loopCondition.Check())
            {

                actions[ptr].Act();
                if (actions[ptr].Finished)
                {
                    actions[ptr] = actions[ptr].Duplicate();
                    ptr++;
                    if (ptr >= actions.Count)
                    {
                        ptr = 0;
                        loopCondition.Update();
                    }
                }
            }
            else
            {
                isFinish = true;
            }
        }

        public override IAction Duplicate()
        {
            var p = new LoopAction(loopCondition.Duplicate());
            foreach (var ac in actions)
            {
                p.actions.Add(ac.Duplicate());
            }
            return p;
        }
    }
}
