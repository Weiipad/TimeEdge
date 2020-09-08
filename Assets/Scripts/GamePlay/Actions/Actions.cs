using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine.Animations;
using UnityEngine;

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
        protected Queue<IAction> actions = null;

        protected bool isFinish = false;
        public override bool Finished { get => isFinish; }

        public void AddSubAction(IAction action)
        {
            if (actions == null) actions = new Queue<IAction>();
            actions.Enqueue(action);
        }

        public override void Act()
        {
            if (actions != null && actions.Count != 0)
            {
                var ac = actions.Peek();
                ac.Act();
                if (ac.Finished)
                {
                    actions.Dequeue();
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
                branch.actions = new Queue<IAction>();
                foreach (var ac in actions)
                {
                    actions.Enqueue(ac.Duplicate());
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
                    actions.Add(ac.Duplicate());
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
                    ptr++;
                    if (ptr >= actions.Count)
                    {
                        ptr = 0;
                        loopCondition.Update();
                        actions.ForEach(action => action = action.Duplicate());
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
            var p = new LoopAction(loopCondition);
            p.isFinish = false;
            p.actions = new List<IAction>();
            foreach (var ac in actions)
            {
                actions.Add(ac.Duplicate());
            }
            return p;
        }
    }
}
