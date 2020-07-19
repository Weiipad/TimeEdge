using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float timeAccumulator;
    public float damage;

    [HideInInspector]
    public new Rigidbody2D rigidbody;

    [HideInInspector]
    public WeaponData weaponData;


    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (weaponData.bulletDuration <= 0) return;

        timeAccumulator += Time.deltaTime;
        if (timeAccumulator >= weaponData.bulletDuration)
        {
            Destroy(transform.gameObject);
        }
    }
}
