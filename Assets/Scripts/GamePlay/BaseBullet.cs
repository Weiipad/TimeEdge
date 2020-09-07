using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    public new Rigidbody2D rigidbody;
    public float damage;
    public float velocity;
    private Vector2? postVelocity = null;

    protected virtual void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rigidbody.velocity = velocity * transform.up;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameStatus.IsPauseGame())
            return;
        if ((collision.CompareTag("Enemy") && gameObject.CompareTag("Bullet0")) || (collision.CompareTag("Player") && gameObject.CompareTag("Bullet1")))
        {
            OnCollide(collision.GetComponent<GameEntity>());
        }
    }

    protected virtual void OnCollide(GameEntity e)
    {
        e.Hurt(this);
        Destroy(gameObject);
    }

    void FixedUpdate()
    {
        if (GameStatus.IsPauseGame())
        {
            if (!postVelocity.HasValue)
            {
                postVelocity = rigidbody.velocity;
                rigidbody.velocity = Vector2.zero;
            }
            return;
        }

        if (postVelocity.HasValue)
        {
            rigidbody.velocity = postVelocity.Value;
            postVelocity = null;
        }
    }
}
