using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GamePlay.Actions
{
    class SinWave : IAction
    {
        private Transform target;
        private readonly Vector3 pin;
        private readonly Vector3 dir;
        private readonly float a;
        private readonly float w;
        private float t;
        public override bool Finished => w * t >= 2f * Mathf.PI;

        public SinWave(Transform target, Vector2 direction, float amplitude, float angularSpeed)
        {
            this.target = target;
            a = amplitude;
            w = angularSpeed;
            dir = new Vector3(direction.x, direction.y, 0).normalized;
            pin = target.position;
            t = 0;
        }

        public override void Act()
        {
            t += Time.deltaTime;
            target.position = pin + dir * a * Mathf.Sin(w * t);
        }

        public override IAction Duplicate()
        {
            return new SinWave(target, new Vector2(dir.x, dir.y), a, w);
        }
    }
}
