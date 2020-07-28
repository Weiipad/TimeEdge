using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversePlane : MonoBehaviour
{
    private Weapon weapon;
    void Start()
    {
        weapon = new CircleGun(GetComponent<GameEntity>(), "ScriptableObjects/WeaponData/CircleGun");
    }

    void Update()
    {
        weapon.Fire();
        weapon.Update();
    }
}
