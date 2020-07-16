using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public BulletData data;

    private float timeAccumulator;

    [HideInInspector]
    public new Rigidbody2D rigidbody;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        timeAccumulator += Time.deltaTime;
        if (timeAccumulator >= data.duration)
        {
            Destroy(transform.gameObject);
        }
    }
}
