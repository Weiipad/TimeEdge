using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Weapon weapon;
    // Start is called before the first frame update
    void Start()
    {
        weapon.Equip(GetComponent<GameEntity>());
    }

    // Update is called once per frame
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
