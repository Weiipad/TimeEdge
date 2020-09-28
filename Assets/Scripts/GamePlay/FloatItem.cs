using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatItem : MonoBehaviour
{
    public SpriteRenderer sr;
    private Vector2 direction = new Vector2();
    private Rigidbody2D body;
    public Effect effect;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        direction.y = Random.Range(-2f, 0f);
        direction.x = Random.Range(-2f, 2f);
        body.velocity = direction;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var entity = collision.GetComponent<GameEntity>();
            if (effect == null)
                effect = new AddShield(entity);
            else
                effect.entity = entity;
            entity.AddEffect(effect);
            Destroy(gameObject);
        }
    }
}
