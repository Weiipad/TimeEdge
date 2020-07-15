using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBullet : MonoBehaviour
{
    public BulletData data;

    private float timeAccumulator;

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = data.sprite;
        
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
