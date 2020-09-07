using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionList
{
    private GameEntity host;
    private Weapon.WeaponInterface wi = null;

    public List<EntityAction> actions;
    public bool Loop = false;

    private int pointer = 0;

    private List<Coroutine> preCoroutine = new List<Coroutine>();
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
        pointer = 0;
        if (actions.Count != 0) preCoroutine.Add(host.StartCoroutine(actions[0].Act(this, host, wi)));
    }

    public void Stop()
    {
        foreach(var i in preCoroutine)
        {
            host.StopCoroutine(i);
        }
        preCoroutine = new List<Coroutine>();
    }

    public void SwitchToNext()
    {
        if (++pointer >= actions.Count || host == null)
        {
            if (Loop)
                pointer = 0;
            else
                return;
        }
        if (pointer >= actions.Count) pointer = 0;
        preCoroutine.Add(host.StartCoroutine(actions[pointer].Act(this, host, wi)));
    }
}
