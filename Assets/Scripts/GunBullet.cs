using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBullet : MonoBehaviour
{
    public BulletData data;

    private new Rigidbody2D rigidbody;
    private float timeAccumulator;

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = data.sprite;
        rigidbody = GetComponent<Rigidbody2D>();
        
    }

    void Update()
    {
        rigidbody.velocity = data.velocity * Time.deltaTime * transform.up;
        timeAccumulator += Time.deltaTime;
        if (timeAccumulator >= data.duration)
        {
            Destroy(transform.gameObject);
        }
    }
}
