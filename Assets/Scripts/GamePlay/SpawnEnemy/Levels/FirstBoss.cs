using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBoss : Level
{
    private GameObject boss;

    private AudioSource musicPlayer;

    public override void StartLevel(LevelList levelList)
    {
        boss = EnemyPrefabs[0];
        boss.SetActive(true);
        MoveVectorByTime strightDown = ActionMaker.MakeActionMoveVectorByTime(new Vector2(0.0f, -2.0f), 6f);
        boss.GetComponent<Enemy>().actions.Add(strightDown);
        boss.GetComponent<Enemy>().StartAction();
        musicPlayer = GameObject.Find("AudioGroup").transform.GetChild(0).GetComponent<AudioSource>();
        musicPlayer.clip = Musics[0];
        musicPlayer.Play();
    }

    public override void EndLevel()
    {

    }
}
