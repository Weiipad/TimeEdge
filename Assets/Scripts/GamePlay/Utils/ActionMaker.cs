using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ActionMaker
{
    public static MoveAndShoot MakeActionMoveAndShoot(Vector2 targetPosition, float speed)
    {
        MoveAndShoot moveAndShoot = ScriptableObject.CreateInstance<MoveAndShoot>();
        moveAndShoot.targetPosition = targetPosition;
        moveAndShoot.speed = speed;
        return moveAndShoot;
    }

    public static MoveCircleByTime MakeActionMoveCircleByTime(Vector2 targetPoint, float radius, float seconds)
    {
        MoveCircleByTime moveCircleByTime = ScriptableObject.CreateInstance<MoveCircleByTime>();
        moveCircleByTime.targetPoint = targetPoint;
        moveCircleByTime.radius = radius;
        moveCircleByTime.seconds = seconds;
        return moveCircleByTime;
    }

    public static MoveTowards MakeActionMoveTowards(float distatnce, float speed)
    {
        MoveTowards moveTowards = ScriptableObject.CreateInstance<MoveTowards>();
        moveTowards.distance = distatnce;
        moveTowards.speed = speed;
        return moveTowards;
    }

    public static MoveVectorByTime MakeActionMoveVectorByTime(Vector2 vector, float seconds)
    {
        MoveVectorByTime moveVectorByTime = ScriptableObject.CreateInstance<MoveVectorByTime>();
        moveVectorByTime.vector = vector;
        moveVectorByTime.seconds = seconds;
        return moveVectorByTime;
    }
}
