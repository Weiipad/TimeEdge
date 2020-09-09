using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GamePlay.Actions
{
    public class SinWavingMoveTo : IAction
    {
        private Transform target;
        private Vector3? pin;
        private readonly Vector3 dir;
        private readonly float a;
        private readonly float w;
        private readonly Vector3 targetPos;
        private readonly float speed;
        private bool isFinish = false;
        private float t;
        public override bool Finished => isFinish;

        public SinWavingMoveTo(Transform target, Vector2 viberatingDirection, float amplitude, float angularSpeed, Vector2 targetPosition, float moveSpeed)
        {
            this.target = target;
            a = amplitude;
            w = angularSpeed;
            dir = new Vector3(viberatingDirection.x, viberatingDirection.y, 0).normalized;
            pin = null;
            targetPos = targetPosition;
            speed = moveSpeed;
            t = 0;
        }

        public override void Act()
        {
            if (pin == null) pin = target.position;
            t += Time.deltaTime;
            target.position = pin.Value + dir * a * Mathf.Sin(w * t);

            var direction = targetPos - pin.Value;
            pin += speed * direction.normalized * Time.deltaTime;
            var aDirection = targetPos - pin.Value;

            isFinish = Vector3.Dot(direction, aDirection) <= 0;
        }

        public override IAction Duplicate()
        {
            return new SinWave(target, new Vector2(dir.x, dir.y), a, w);
        }
    }
}
