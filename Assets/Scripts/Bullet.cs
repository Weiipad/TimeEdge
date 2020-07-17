using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float timeAccumulator;
    public bool isFromPlayer;
    public BulletData data;

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
        if (data.duration <= 0) return;

        timeAccumulator += Time.deltaTime;
        if (timeAccumulator >= data.duration)
        {
            Destroy(transform.gameObject);
        }
    }
}
