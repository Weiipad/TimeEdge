using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleEnemy : MonoBehaviour
{
    public Weapon weapon;
    void Start()
    {
        weapon.Equip(GetComponent<GameEntity>());
    }

    void Update()
    {
        weapon.Fire();
        weapon.Update();
    }
}
