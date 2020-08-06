using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponHolder : MonoBehaviour
{
    public Weapon weapon;

    public Weapon.WeaponInterface wi = null;
    private void Start()
    {
        if (weapon != null)
            wi = new Weapon.WeaponInterface(GetComponent<GameEntity>(), weapon);
    }

    void LateUpdate()
    {
        wi.LateUpdate();
    }
}
