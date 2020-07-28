﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Weapon weapon;
    void Start()
    {
        weapon = new MachineGun(GetComponent<GameEntity>(), "ScriptableObjects/WeaponData/EnemyMachineGun");
    }

    void Update()
    {
        weapon.Fire();
        weapon.Update();
    }
}
