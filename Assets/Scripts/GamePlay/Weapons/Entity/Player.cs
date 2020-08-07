﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    public int avaliableShock;
    public GameObject shockBombObject;
    public Weapon weapon;

    public Weapon.WeaponInterface wi = null;
    private void Start()
    {
        if (weapon != null)
            wi = new Weapon.WeaponInterface(GetComponent<GameEntity>(), weapon);
    }

    void Update()
    {
        wi.Update();
        if (Input.GetKey(KeyCode.Mouse0)) wi.Shoot();
        if (Input.GetKeyDown(KeyCode.Mouse1)) GenShockBomb();
    }

    private void GenShockBomb()
    {
        Instantiate(shockBombObject, transform.position, Quaternion.identity);
    }
}
