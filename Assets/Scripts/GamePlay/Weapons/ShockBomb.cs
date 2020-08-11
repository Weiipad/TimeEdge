using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockBomb : MonoBehaviour
{
    public CircleCollider2D cc;
    public LineRenderer lineRenderer;

    public float spreadAcceleration;
    public float duration;

    private float spreadVelocity;
    private float timeElapsed;
    void Start()
    {
        spreadVelocity = 10;
        timeElapsed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameStatus.IsPauseGame())
            return;
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= duration)
        {
            Destroy(gameObject);
        }

        spreadVelocity += spreadAcceleration * Time.deltaTime;
        cc.radius += spreadVelocity * Time.deltaTime;
        DrawCircle(cc.radius);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet1"))
        {
            Destroy(collision.gameObject);
        }
    }

    private static int POINTS = 100;
    private static float STEP_ANGLE = 360f / POINTS;
    private void DrawCircle(float radius)
    {
        lineRenderer.positionCount = POINTS + 1;

        for (var i = 0; i <= POINTS; i++)
        {
            var point = Quaternion.Euler(0, 0, STEP_ANGLE * i) * transform.up * radius + transform.position;
            lineRenderer.SetPosition(i, point);
        }
    }
}
