using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float loadSpeedScale;

    private Weapon weapon;
    public Weapon GetWeapon { get => weapon; }
    // Start is called before the first frame update
    void Start()
    {
        weapon = new MachineGun(this);
    }

    // Update is called once per frame
    void Update()
    {
        weapon.Update();
    }
}
