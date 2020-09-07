using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;

[CreateAssetMenu(fileName = "NewAction", menuName = "Time Edge/Action/MoveVectorByTime")]
public class MoveVectorByTime : EntityAction
{
    public Vector2 vector;
    public float vectorScale = 1f;

    public float seconds;
    public float secondScale = 1f;

    public bool MirrorX;
    public bool MirrorY;
    public override IEnumerator Act(ActionList list, GameEntity entity, Weapon.WeaponInterface wi)
    {
        if (BeforeActionDelegate != null)
            BeforeActionDelegate();
        if (secondScale <= 0f)
            yield break;
        float curSeconds = 0f;
        Vector2 prePosition = entity.transform.position;
        Vector2 offset = vector * vectorScale * (0.02f * secondScale / seconds);
        if (MirrorX)
            offset.x = -offset.x;
        if (MirrorY)
            offset.y = -offset.y;
        WaitForSeconds wait = new WaitForSeconds(0.02f * secondScale);
        while (curSeconds < seconds)
        {
            if (GameStatus.IsPauseGame())
                yield return 0;
            else
            {
                entity.transform.position = (Vector2)entity.transform.position + offset;
                yield return wait;
                curSeconds += 0.02f * secondScale;
            }
        }

        if (seconds == 0f || curSeconds == 0f)
            entity.transform.position = (Vector2)prePosition + vector;

        if (AfterActionDelegate != null)
            AfterActionDelegate();
        if (list != null && !IsStopSwitch)
            list.SwitchToNext();
    }
}
