using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstLevel : Level
{
    private bool isStartThisLevel;
    private GameObject player;
    private Coroutine preCoroutine;
    public override void StartLevel()
    {
        isStartThisLevel = true;

    }

    public override void EndLevel()
    {
        isStartThisLevel = false;
    }

    //private IEnumerator DoFirstLevel()
    //{
    //    player = new GameObject("Player");
    //    player.tag = "player";

    //}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
