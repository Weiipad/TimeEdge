using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstLevel : Level
{
    public AudioClip Music;

    private bool isStartThisLevel;
    private Coroutine preCoroutine;

    private AudioSource musicPlayer;

    public override void StartLevel()
    {
        isStartThisLevel = true;
        musicPlayer = GameObject.Find("AudioGroup").transform.GetChild(0).GetComponent<AudioSource>();
        musicPlayer.clip = Music;
        musicPlayer.Play();
        //preCoroutine = StartCoroutine(DoFirstLevel());
    }

    public override void EndLevel()
    {
        isStartThisLevel = false;
        if(preCoroutine != null)
            StopCoroutine(preCoroutine);
    }

    private IEnumerator DoFirstLevel()
    {
        yield return 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
