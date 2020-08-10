using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeting : MonoBehaviour
{
    public Transform target;

    private void Update()
    {
        transform.rotation = Quaternion.FromToRotation(Vector3.up, target.transform.position - transform.position);
    }
}
