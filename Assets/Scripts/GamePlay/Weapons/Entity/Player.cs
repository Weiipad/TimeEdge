using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : WeaponHolder
{
    void Update()
    {
        wi.Update();
        if (Input.GetKey(KeyCode.Mouse0)) wi.Shoot();
    }
}
