using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityAction : ScriptableObject
{
    public abstract IEnumerator Act(ActionList list, GameEntity entity, Weapon.WeaponInterface wi);
}
