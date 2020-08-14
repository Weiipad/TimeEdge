using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Level : MonoBehaviour
{
    public List<AudioClip> Musics;
    public List<GameObject> EnemyPrefabs;
    public List<Weapon> Weapons;

    protected bool isThisLevelStart;
    public bool IsThisLevelStart { get { return isThisLevelStart; } }

    protected bool isThisLevelEnd;
    public bool IsThisLevelEnd { get { return isThisLevelEnd; } }

    public abstract void StartLevel();
    public abstract void EndLevel();
}
