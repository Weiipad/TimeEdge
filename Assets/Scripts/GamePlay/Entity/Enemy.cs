using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Weapon weapon;

    public Weapon.WeaponInterface wi = null;

    public List<EntityAction> actions;
    public bool Loop;

    private ActionList list;

    private void Start()
    {
        var entity = GetComponent<GameEntity>();
        if (weapon != null)
            wi = new Weapon.WeaponInterface(entity, weapon);
    }

    void Update()
    {
        if (GameStatus.IsPauseGame())
            return;
        if (wi != null)
        {
            wi.Update();
            wi.Shoot();
        }
    }

    public void StartAction()
    {
        var entity = GetComponent<GameEntity>();
        list = new ActionList(entity, actions);
        list.Loop = Loop;
        list.Start();
    }

    public void ActionLoop(bool isLoop)
    {
        Loop = isLoop;
        if(list != null)
            list.Loop = Loop;
    }
}
