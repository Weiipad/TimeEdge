using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBullet : MonoBehaviour
{
    public BulletData data;

    private float timeAccumulator;
    private new Rigidbody2D rigidbody;

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = data.sprite;
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
