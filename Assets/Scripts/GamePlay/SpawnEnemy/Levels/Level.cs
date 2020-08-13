using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Level : MonoBehaviour
{
    public float StartTime;
    public float Duration;
    public AudioClip[] Musics;
    public GameObject[] EnemyPrefabs;
    public Weapon[] Weapons;
    public abstract void StartLevel();
    public abstract void EndLevel();
}
