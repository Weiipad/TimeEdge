using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAction", menuName = "Time Edge/Action/MoveByCircle")]
public class MoveByCircle : EntityAction
{
    public enum CircleCenterPosition
    {
        up,
        down,
    }
    public Vector2 targetPoint = Vector2.zero;

    public CircleCenterPosition circleCenterPosition;
    public float radius = 1f;
    
    public float seconds = 1f;
    public float secondScale = 1f;

    public bool MirrorX;
    public bool MirrorY;

    public bool IsClockDirect;

    public override IEnumerator Act(ActionList list, GameEntity entity, Weapon.WeaponInterface wi)
    {
        float curSeconds = 0f;
        float curAngle;
        //=========Start calculate the center of circle=========
        Vector2 centerOfCircle;
        Vector2 prepenVector;
        Vector2 targetCopy = targetPoint;
        if (MirrorX)
            targetCopy.x = -targetCopy.x;
        if(MirrorY)
            targetCopy.y = -targetCopy.y;
        Vector2 entityToTargetVector = targetCopy - (Vector2)entity.transform.position;
        float vectorLength = Mathf.Sqrt(radius * radius - (entityToTargetVector.magnitude / 2f) * (entityToTargetVector.magnitude / 2f));
        if (entityToTargetVector.magnitude / 2f > radius)
            throw new System.Exception("The radius is so smaller!");
        if (circleCenterPosition == CircleCenterPosition.up)
            prepenVector = new Vector2(-entityToTargetVector.y, entityToTargetVector.x);
        else
            prepenVector = new Vector2(entityToTargetVector.y, -entityToTargetVector.x);
        prepenVector = prepenVector.normalized * vectorLength;
        centerOfCircle = new Vector2(prepenVector.x + (targetCopy.x + entity.transform.position.x) / 2f, prepenVector.y + (targetCopy.y + entity.transform.position.y) / 2f);
        //==========End calculate the center of circle===========

        //==========Start calculate the offset of angle==========
        float startAngle = Mathf.Acos((entity.transform.position.x - centerOfCircle.x) / radius) * 180f / Mathf.PI;
        if (entity.transform.position.y < centerOfCircle.y)
            startAngle = -startAngle;
        float endAngle = Mathf.Acos((targetPoint.x - centerOfCircle.x) / radius) * 180f / Mathf.PI;
        if (targetPoint.y < centerOfCircle.y)
            endAngle = -endAngle;
        float angleOffset;
        if (!IsClockDirect)
        {
            if (startAngle <= endAngle)
                angleOffset = endAngle - startAngle;
            else
                angleOffset = endAngle + 360f - startAngle;
        }
        else
        {
            if (startAngle <= endAngle)
                angleOffset = startAngle - endAngle;
            else
                angleOffset = - (360f - endAngle - startAngle);
        }
        angleOffset *= (0.02f * secondScale / seconds);
        curAngle = startAngle;
        //===========End calculate the offset of angle===========

        while (curSeconds < seconds)
        {
            if (GameStatus.IsPauseGame())
                continue;
            curAngle += angleOffset;
            Vector2 newPos = CalculatePos(curAngle, centerOfCircle);
            entity.transform.position = newPos;
            if (entity.transform.position.Equals(targetPoint))
                yield break;
            yield return new WaitForSecondsRealtime(0.02f);
            curSeconds += 0.02f;
        }
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
