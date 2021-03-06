using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GamePlay.Actions;
using System.Xml.Schema;

public class TestActions : MonoBehaviour
{
    Branch root, stageOne;

    public BaseBullet bullet;

    class Shoot : IAction
    {
        bool isFinish = false;
        public override bool Finished => isFinish;
        public BaseBullet bullet;
        Transform parent;

        float timeElapsed;
        float deltaTime;

        public Shoot(Transform parent, BaseBullet bullet, float deltaTime)
        {
            this.parent = parent;
            this.bullet = bullet;
            this.deltaTime = deltaTime;
            timeElapsed = 0;
        }

        public override void Act()
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed >= deltaTime)
            {
                Instantiate(bullet, parent.position, Quaternion.Euler(0, 0, 90));
                isFinish = true;
            }
        }

        public override IAction Duplicate()
        {
            return new Shoot(parent, bullet, deltaTime);
        }
    }

    private void Awake()
    {
        root = new Branch();

        stageOne = new Branch();

        var moveToTarget = new MoveTo(transform, new Vector2(-8.5f, 3), 10);

        var p = new Parallel();

        var waving = new LoopAction(new LoopInTimes(-1));

        var combine = new LoopAction(new LoopInTimes(4));
        combine.PushAction(new SinWave(transform, Vector2.right, 4f, 2f));

        waving.PushAction(new MoveTo(transform, new Vector2(0, 3), 10));
        waving.PushAction(combine);
        waving.PushAction(new MoveTo(transform, new Vector2(8.5f, 3), 10));
        waving.PushAction(new MoveTo(transform, new Vector2(8.5f, -3), 10));
        waving.PushAction(new MoveTo(transform, new Vector2(-8.5f, -3), 10));
        waving.PushAction(moveToTarget.Duplicate());

        var looper = new LoopAction(new LoopWhileActing(combine));
        looper.PushAction(new Shoot(transform, bullet, 0.1f));

        p.AddSubAction(looper);
        p.AddSubAction(waving);

        stageOne.AddSubAction(moveToTarget);
        stageOne.AddSubAction(p);
        root.AddSubAction(stageOne);
        root.AddSubAction(new MoveTo(transform, new Vector2(0, 0), 10f));
    }

    private void Update()
    {
        if (!root.Finished && !GameStatus.IsPauseGame()) root.Act();
    }
}
