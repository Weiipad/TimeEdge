using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAction", menuName = "Time Edge/Action/TestArcMove")]
public class TestArcMove : EntityAction
{
    public override IEnumerator Act(ActionList list, GameEntity entity, Weapon.WeaponInterface wi)
    {
        yield return 0;
        list.SwitchToNext();
    }
}
