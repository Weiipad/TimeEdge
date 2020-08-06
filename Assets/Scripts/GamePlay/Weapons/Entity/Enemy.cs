using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : WeaponHolder
{
    void Update()
    {
        weapon.Fire();
        weapon.Update();
    }
}
