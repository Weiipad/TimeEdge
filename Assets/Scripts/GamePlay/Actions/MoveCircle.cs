using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.Actions
{
    class MoveCircle : IAction
    {
        public enum CircleCenterPosition
        {
            up,
            down,
        }

        
        private Transform trans;
        private Vector3 target;
        private float seconds;
        private float radius;
        private CircleCenterPosition circleCenterPosition;
        private bool isClockDirect;

        private Vector2 centerPosition;
        private float startAngle;
        private float endAngle;
        private float angleOffset;

        private float curAngle;
        private float curSeconds;

        private bool isFinish = false;
        public override bool Finished => isFinish;

        private MoveCircle() { }
        public MoveCircle(Transform trans, Vector3 target, float radius, float seconds, int circleCenterPosition, bool isClockDirect)
        {
            this.trans = trans;
            this.target = target;
            this.seconds = seconds;
            this.radius = radius;
            this.circleCenterPosition = (CircleCenterPosition)circleCenterPosition;
            this.isClockDirect = isClockDirect;
            curSeconds = 0f;
        }
        
        public override void Act()
        {
            curAngle += angleOffset;
            Vector2 newPos = CalculatePos(curAngle, centerPosition);
            trans.position = newPos;
            curSeconds += 0.02f;
            isFinish = curAngle.Equals(endAngle) || trans.position.Equals(target) || curSeconds.Equals(seconds);
        }

        public override IAction Duplicate()
        {
            return new MoveCircle(trans, target, radius, seconds, (int)circleCenterPosition, isClockDirect);
        }

        private void Init(Transform trans, Vector3 targetPoint)
        {
            //=========Start calculate the center of circle=========
            Vector2 prepenVector;
            Vector2 targetCopy = targetPoint;
            Vector2 entityToTargetVector = targetCopy - (Vector2)trans.position;
            float vectorLength = Mathf.Sqrt(radius * radius - (entityToTargetVector.magnitude / 2f) * (entityToTargetVector.magnitude / 2f));
            if (entityToTargetVector.magnitude / 2f > radius)
                throw new System.Exception("The radius is so smaller!");
            if (circleCenterPosition == CircleCenterPosition.up)
                prepenVector = new Vector2(-entityToTargetVector.y, entityToTargetVector.x);
            else
                prepenVector = new Vector2(entityToTargetVector.y, -entityToTargetVector.x);
            prepenVector = prepenVector.normalized * vectorLength;
            centerPosition = new Vector2(prepenVector.x + (targetCopy.x + trans.position.x) / 2f, prepenVector.y + (targetCopy.y + trans.position.y) / 2f);
            //==========End calculate the center of circle===========

            //==========Start calculate the offset of angle==========
            startAngle = Mathf.Acos((trans.position.x - centerPosition.x) / radius) * 180f / Mathf.PI;
            if (trans.position.y < centerPosition.y)
                startAngle = -startAngle;
            endAngle = Mathf.Acos((targetPoint.x - centerPosition.x) / radius) * 180f / Mathf.PI;
            if (targetPoint.y < centerPosition.y)
                endAngle = -endAngle;

            if (startAngle <= endAngle)
                angleOffset = endAngle - startAngle;
            else
                angleOffset = endAngle + 360f - startAngle;
            if (isClockDirect)
            {
                if (angleOffset > 0f)
                    angleOffset = -(360f - angleOffset);
                else
                    angleOffset = 360f + angleOffset;
            }
            angleOffset *= (0.02f / seconds);
            curAngle = startAngle;
            //===========End calculate the offset of angle===========
        }

        private Vector2 CalculatePos(float angle, Vector2 centerOfCircle)
        {
            if (angle >= 360f)
                angle = angle - 360f;
            else if (angle < 0f)
                angle = 360f + angle;
            return (new Vector2(radius * Mathf.Cos(angle * Mathf.PI / 180f), radius * Mathf.Sin(angle * Mathf.PI / 180f)) + centerOfCircle);
        }
    }
}
