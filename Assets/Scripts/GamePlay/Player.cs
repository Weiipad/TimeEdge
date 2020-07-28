using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Test test;
    private Weapon weapon;
    public Weapon GetWeapon { get => weapon; }
    // Start is called before the first frame update
    void Start()
    {
        weapon = new MachineGun(GetComponent<GameEntity>());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0)) weapon.Fire();
        weapon.Update();
    }
}
