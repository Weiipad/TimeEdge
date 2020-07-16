using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float loadSpeedScale;

    private Weapon weapon;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(null);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
