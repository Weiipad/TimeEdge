using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityAction : ScriptableObject
{
    public delegate void ActionDelegate();
    public abstract IEnumerator Act(ActionList list, GameEntity entity, Weapon.WeaponInterface wi);
    public ActionDelegate BeforeActionDelegate;
    public ActionDelegate AfterActionDelegate;
    public bool IsStopSwitch = false;
}
