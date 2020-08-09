using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Weapon weapon;

    public Weapon.WeaponInterface wi = null;

    public List<EntityAction> actions;

    private ActionList list;

    private void Start()
    {
        var entity = GetComponent<GameEntity>();
        if (weapon != null)
            wi = new Weapon.WeaponInterface(entity, weapon);

        list = new ActionList(entity, actions);
        list.Start();
    }

    void Update()
    {
        
        wi.Update();
        wi.Shoot();
    }
}
