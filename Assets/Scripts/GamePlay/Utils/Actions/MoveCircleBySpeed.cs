using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAction", menuName = "Time Edge/Action/MoveCircleBySpeed")]
public class MoveCircleBySpeed : EntityAction
{
    public enum CircleCenterPosition
    {
        up,
        down,
    }
    public Vector2 targetPoint = Vector2.zero;

    public CircleCenterPosition circleCenterPosition;
    public float radius = 1f;

    public float speed = 0.1f;

    public bool IsClockDirect;

    public override IEnumerator Act(ActionList list, GameEntity entity, Weapon.WeaponInterface wi)
    {
        //=========Start calculate the center of circle=========
        Vector2 centerOfCircle;
        Vector2 prepenVector;
        Vector2 targetCopy = targetPoint;
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

        while (Vector2.Distance(entity.transform.position, targetPoint) >= 0.001f)
        {
            if (GameStatus.IsPauseGame())
                continue;
            Vector2 centerToCur = (Vector2)entity.transform.position - centerOfCircle;
            float sin = Vector2.Dot(centerToCur, Vector2.up) / radius;
            float cos = Vector2.Dot(centerToCur, Vector2.right) / radius;
            Vector2 velocity = new Vector2(speed * sin, speed * cos);
            entity.transform.position += (Vector3)velocity * Time.deltaTime;
            yield return 0;
        }
        list.SwitchToNext();
    }
}
