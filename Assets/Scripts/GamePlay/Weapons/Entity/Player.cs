using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : WeaponPos
{
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0)) weapon.Fire();
        weapon.Update();
    }

    private void LateUpdate()
    {
        weapon.LateUpdate();
    }
}
