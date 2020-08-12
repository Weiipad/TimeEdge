using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAction", menuName = "Time Edge/Action/MoveTowards")]
public class MoveTowards : EntityAction
{
    public float distance;
    public float speed;
    public override IEnumerator Act(ActionList list, GameEntity entity, Weapon.WeaponInterface wi)
    {
        var target = entity.GetComponent<Targeting>().target;
        if (target == null)
        {
            list.SwitchToNext();
            yield return 0;
        }

        while (true)
        {
            if (GameStatus.IsPauseGame())
                yield return 0;
            else
            {
                var curDistance = Vector3.Distance(target.position, entity.transform.position);
                if (curDistance > distance)
                {
                    entity.transform.position += (curDistance / distance) * speed * Time.deltaTime * entity.transform.up;
                }
                else
                {
                    entity.transform.position += speed * Time.deltaTime * entity.transform.right;
                }
                yield return 0;
            }
        }
    }
}
