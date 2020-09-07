using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Legacy code, too lazy to rewrite.
public class Bullet : BaseBullet
{
    private float timeAccumulator;

    [HideInInspector]
    public float duration;

    private LayerMask layerMask;
    protected override void Awake()
    {
        base.Awake();
        layerMask = new LayerMask();
        layerMask.value = LayerMask.NameToLayer("Default");
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
