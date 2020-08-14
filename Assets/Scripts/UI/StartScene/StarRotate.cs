using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarRotate : MonoBehaviour
{
    public float RotateEuler = 10f;
    void Update()
    {
        if (GameStatus.IsPauseGame())
            return;
        gameObject.transform.Rotate(new Vector3(0f, 0f, RotateEuler * Time.deltaTime));
    }
}
