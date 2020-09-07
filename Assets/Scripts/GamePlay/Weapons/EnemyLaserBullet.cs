using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserBullet : Bullet
{

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (GameStatus.IsPauseGame())
            return;
        if (collision.CompareTag("Player") && gameObject.CompareTag("Bullet1"))
        {
            OnCollide(collision.GetComponent<GameEntity>());
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameStatus.IsPauseGame())
            return;
        if (collision.CompareTag("Player") && gameObject.CompareTag("Bullet1"))
        {
            OnCollide(collision.GetComponent<GameEntity>());
        }
    }

    protected override void OnCollide(GameEntity e)
    {
        e.Hurt(this);
    }
}
