using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeting : MonoBehaviour
{
    public Transform target;

    public float speed;

    private void Update()
    {
        if (GameStatus.IsPauseGame())
            return;

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.FromToRotation(Vector3.up, target.transform.position - transform.position), speed);
    }
}
