using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour
{
    public Enemy[] Enemies = null;
    bool isStart = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isStart)
        {
            foreach(var i in Enemies)
            {
                i.StartAction();
            }
            isStart = true;
        }
    }
}
