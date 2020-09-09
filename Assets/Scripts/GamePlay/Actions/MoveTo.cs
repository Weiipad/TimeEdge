using System;
using UnityEngine;

namespace GamePlay.Actions
{
    public class MoveTo : IAction
    {
        private Transform trans;
        private Vector3 target;
        private float speed;
        private bool isFinish = false;

        public override bool Finished => isFinish;

        public MoveTo(Transform trans, Vector2 target, float speed)
        {
            this.target = new Vector3(target.x, target.y, trans.position.z);
            this.trans = trans;
            this.speed = speed;
        }

        public override void Act()
        {
            var dir = target - trans.position;
            trans.position += speed * dir.normalized * Time.deltaTime;
            var aDir = target - trans.position;

            isFinish = Vector3.Dot(dir, aDir) <= 0;

            if (isFinish) Debug.Log($"Moved to {target}");
        }

        public override IAction Duplicate()
        {
            return new MoveTo(trans, target, speed);
        }
    }
}
