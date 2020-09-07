using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float timeAccumulator;

    public float damage;
    public float velocity;

    [HideInInspector]
    public new Rigidbody2D rigidbody;

    [HideInInspector]
    public float duration;

    private LayerMask layerMask;
    private Vector2? postVelocity;
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        layerMask = new LayerMask();
        layerMask.value = LayerMask.NameToLayer("Default");
    }

    private void Start()
    {
        rigidbody.velocity = velocity * transform.up;
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

    protected void OnTriggerEnter2D(Collider2D collision)
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

    protected virtual void Update()
    {
        if (GameStatus.IsPauseGame())
            return;
        if (duration <= 0) return;

        timeAccumulator += Time.deltaTime;
        if (timeAccumulator >= duration)
        {
            Destroy(transform.gameObject);
        }
    }
}
