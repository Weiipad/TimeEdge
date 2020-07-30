using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet0") || collision.CompareTag("Bullet1"))
        {
            if (collision.name != "Light") Destroy(collision.gameObject);
        }
    }
}
