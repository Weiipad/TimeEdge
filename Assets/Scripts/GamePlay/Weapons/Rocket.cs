using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public CircleCollider2D probe;
    public Bullet bullet;
    public GameEntity target = null;
    private bool finding = true;

    void Update()
    {
        if (GameStatus.IsPauseGame())
            return;
        if (target == null)
        {
            if (!finding) 
            {
                probe.radius = 0.000001f;
                finding = true;
            }
            probe.radius += 10.0f * Time.deltaTime;
        }
        else
        {
            var dir = (target.transform.position - bullet.transform.position).normalized;
            var rotation = Quaternion.FromToRotation(bullet.transform.up, dir);
            bullet.transform.rotation = Quaternion.RotateTowards(bullet.transform.rotation, rotation, 2);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            target = collision.GetComponent<GameEntity>();
            finding = false;
        }
    }
}
