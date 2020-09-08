﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GamePlay.Actions;

public class TestActions : MonoBehaviour
{
    Branch root, stageOne;

    public BaseBullet bullet;

    class LoopWhileActing : LoopAction.LoopCondition
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

    class Shoot : IAction
    {
        bool isFinish = false;
        public override bool Finished => isFinish;
        public BaseBullet bullet;
        Transform parent;

        float timeElapsed;
        float deltaTime;

        int a;

        public Shoot(Transform parent, BaseBullet bullet, float deltaTime, int a)
        {
            Debug.Log($"Create! : {a}");

            this.parent = parent;
            this.bullet = bullet;
            this.deltaTime = deltaTime;
            timeElapsed = 0;
            this.a = a;
        }

        public override void Act()
        {
            
            timeElapsed += Time.deltaTime;
            if (timeElapsed >= deltaTime)
            {
                Debug.Log($"{a} : Shoot!!");
                Instantiate(bullet, parent.position, Quaternion.Euler(0, 0, 180));
                isFinish = true;
            }
        }

        public override IAction Duplicate()
        {
            Debug.Log($"Duplicate! : {a+1}");
            return new Shoot(parent, bullet, deltaTime, a + 1);
        }
    }

    private void Awake()
    {
        root = new Branch();

        stageOne = new Branch();

        var p = new Parallel();

        var looper = new LoopAction(new LoopInTimes(3));
        looper.PushAction(new Shoot(transform, bullet, 0.5f, 0));


        var waving = new LoopAction(new LoopInTimes(4));
        waving.PushAction(new MoveTo(transform, new Vector2(-8.42f, 3), 5));
        waving.PushAction(new MoveTo(transform, new Vector2(8.42f, 3), 5));

        p.AddSubAction(looper);
        p.AddSubAction(waving);

        stageOne.AddSubAction(p);
        root.AddSubAction(stageOne);
    }

    private void Update()
    {
        if (!root.Finished) root.Act();
    }
}
