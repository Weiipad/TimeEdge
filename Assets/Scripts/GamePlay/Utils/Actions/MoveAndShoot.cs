using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMASAction", menuName = "Time Edge/Action/MoveAndShoot")]
public class MoveAndShoot : EntityAction
{
    public Vector2 targetPosition;
    public float speed;
    public override IEnumerator Act(ActionList list, GameEntity entity, Weapon.WeaponInterface wi)
    {
        var target = new Vector3(targetPosition.x, targetPosition.y, entity.transform.position.z);
        while (Vector3.Distance(entity.transform.position, target) >= 0.001f)
        {
            if (!GameStatus.IsPauseGame())
            {
                entity.transform.position = Vector3.Lerp(entity.transform.position, target, speed);
                yield return 0;
            }
            else
                yield return 0;
        }
        list.SwitchToNext();
        yield return 0;
    }
}
