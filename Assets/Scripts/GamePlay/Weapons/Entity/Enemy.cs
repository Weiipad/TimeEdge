using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : WeaponPos
{
    void Update()
    {
        weapon.Fire();
        weapon.Update();
    }
}
