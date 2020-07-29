using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float timeAccumulator;
    [HideInInspector]
    public float damage;

    [HideInInspector]
    public new Rigidbody2D rigidbody;

    [HideInInspector]
    public float duration;
    [HideInInspector]
    public float velocity;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rigidbody.velocity = velocity * transform.up;
    }

    protected virtual void Update()
    {
        if (duration <= 0) return;

        timeAccumulator += Time.deltaTime;
        if (timeAccumulator >= duration)
        {
            Destroy(transform.gameObject);
        }
    }
}
