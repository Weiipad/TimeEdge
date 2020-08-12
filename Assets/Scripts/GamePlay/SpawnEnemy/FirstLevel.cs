using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityScript.Scripting.Pipeline;

public class FirstLevel : Level
{
    private bool isStartThisLevel;
    private Coroutine preCoroutine;

    private AudioSource musicPlayer;
    public override void StartLevel()
    {
        if (!isStartThisLevel)
        {
            isStartThisLevel = true;
            musicPlayer = GameObject.Find("AudioGroup").transform.GetChild(0).GetComponent<AudioSource>();
            musicPlayer.clip = Musics[0];
            musicPlayer.Play();
            preCoroutine = StartCoroutine(DoFirstLevel());
        }
    }

    public override void EndLevel()
    {
        if (isStartThisLevel)
        {
            isStartThisLevel = false;
            musicPlayer.Pause();
            if (preCoroutine != null)
                StopCoroutine(preCoroutine);
        }
    }

    private IEnumerator DoFirstLevel()
    {
        for (int i = 0; i < 2; i++)
        {
            if (GameStatus.IsPauseGame())
            {
                i--;
                yield return 0;
            }
            else
            {
                var go = GameObject.Instantiate(EnemyPrefabs[0], transform);
                go.transform.localPosition = Vector3.zero;
                go.transform.parent = null;
                var enemyEntity = go.GetComponent<GameEntity>();
                var enemy = go.GetComponent<Enemy>();
                enemy.weapon = Weapons[0];
                enemy.wi = new Weapon.WeaponInterface(enemyEntity, enemy.weapon);
                enemy.actions = new List<EntityAction>();
                enemy.actions.Add(EntityActions[0]);
                enemy.StartAction();
                yield return new WaitForSeconds(1f);
                Destroy(go);
            }
        }

        for(int i = 0;i < 1;i ++)
        {
            if (GameStatus.IsPauseGame())
            {
                i--;
                yield return 0;
            }
            else
            {
                var go = GameObject.Instantiate(EnemyPrefabs[1], transform);
                go.transform.localPosition = Vector3.zero;
                go.transform.parent = null;
                var enemyEntity = go.GetComponent<GameEntity>();
                var enemy = go.GetComponent<Enemy>();
                enemy.weapon = Weapons[1];
                enemy.wi = new Weapon.WeaponInterface(enemyEntity, enemy.weapon);
                enemy.actions = new List<EntityAction>();
                enemy.actions.Add(EntityActions[0]);
                enemy.StartAction();
                yield return new WaitForSeconds(1f);
                Destroy(go);
            }
        }
        yield break;
    }
}
