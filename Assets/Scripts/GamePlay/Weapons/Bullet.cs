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

    private LayerMask layerMask;
    private Vector2 preVelocity;
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
            if(!(Mathf.Abs((rigidbody.velocity - Vector2.zero).magnitude) <= 0.001f))
            {
                preVelocity = rigidbody.velocity;
                rigidbody.velocity = Vector2.zero;
            }
            return;
        }
        else if (Mathf.Abs((rigidbody.velocity - Vector2.zero).magnitude) <= 0.001f)
        {
            rigidbody.velocity = preVelocity;
        }
        rigidbody.velocity = velocity * transform.up;
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
