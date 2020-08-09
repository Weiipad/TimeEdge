using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionList
{
    private GameEntity host;
    private Weapon.WeaponInterface wi = null;

    public List<EntityAction> actions;

    private int pointer = 0;

    public ActionList(GameEntity host, List<EntityAction> actions)
    {
        this.host = host;
        this.actions = actions;
        var enemy = host.GetComponent<Enemy>();
        if (enemy != null)
            wi = enemy.wi;
    }

    public void Start()
    {
        if (actions.Count != 0) host.StartCoroutine(actions[0].Act(this, host, wi));
    }

    public void SwitchToNext()
    {
        if (++pointer >= actions.Count)
        {
            pointer = 0;
        }
        host.StartCoroutine(actions[pointer].Act(this, host, wi));
    }
}
