using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayAndPause : MonoBehaviour
{
    public PauseGame pauseGameGO;

    private AudioSource player;
    private float recordTime;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<AudioSource>();
        pauseGameGO.OnPressPauseGameButton += RecordMusicCurrentTime;
        pauseGameGO.OnPressReturnGameButton += ContinuePlayMusic;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void RecordMusicCurrentTime()
    {
        recordTime = player.time;
        player.Pause();
    }

    private void ContinuePlayMusic()
    {
        player.time = recordTime;
        player.Play();
    }
}
