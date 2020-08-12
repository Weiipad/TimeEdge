using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                //EnemyInit(go, Vector3.zero, Weapons[0], new EntityAction[] { EntityActions[0] });
                MoveVectorByTime moveVectorByTime = ScriptableObject.CreateInstance<MoveVectorByTime>();
                moveVectorByTime.vector = new Vector2(0, -12);
                moveVectorByTime.seconds = 1f;
                EnemyInit(go, Vector3.zero, Weapons[0], new EntityAction[] { moveVectorByTime });
                yield return new WaitForSeconds(1f);
                Destroy(go);
            }
        }

        for (int i = 0; i < 1; i++)
        {
            if (GameStatus.IsPauseGame())
            {
                i--;
                yield return 0;
            }
            else
            {
                var go = GameObject.Instantiate(EnemyPrefabs[1], transform);
                EnemyInit(go, Vector3.zero, Weapons[1], new EntityAction[] { EntityActions[0] });
                yield return new WaitForSeconds(1f);
                Destroy(go);
            }
        }
        yield return new WaitForSeconds(0.24f);

        List<GameObject> needDestory = new List<GameObject>();
        for (int i = 0; i < 15; i++)
        {
            if (GameStatus.IsPauseGame())
            {
                i--;
                yield return 0;
            }
            else
            {
                var go = GameObject.Instantiate(EnemyPrefabs[0], transform);
                Vector2 spawnLocalPos;
                
                if (i % 2 == 0)
                    spawnLocalPos = new Vector2(-3f, 0f);
                else
                    spawnLocalPos = new Vector2(3f, 0f);

                EntityAction[] actions = new EntityAction[3] { EntityActions[1], null, null };
                if (i != 14)
                {
                    if (i % 2 == 0)
                        actions[1] = EntityActions[3];
                    else
                        actions[1] = EntityActions[2];
                }
                else
                {
                    actions[1] = EntityActions[8];
                    if (i % 2 == 0)
                        actions[2] = EntityActions[3];
                    else
                        actions[2] = EntityActions[2];
                }
                EnemyInit(go, spawnLocalPos, Weapons[0], actions);
                if(i == 14)
                {
                    var goCir = GameObject.Instantiate(EnemyPrefabs[1], transform);
                    EntityAction[] entityActions = new EntityAction[] { EntityActions[4], EntityActions[7] };
                    EnemyInit(goCir, new Vector2(-3f, 0f), Weapons[1], entityActions);
                    needDestory.Add(goCir);

                    goCir = GameObject.Instantiate(EnemyPrefabs[1], transform);
                    entityActions[0] = EntityActions[5];
                    entityActions[1] = EntityActions[6];
                    EnemyInit(goCir, new Vector2(3f, 0f), Weapons[1], entityActions);
                    needDestory.Add(goCir);
                }
                yield return new WaitForSeconds(0.5f);
                if (i == 14)
                {
                    yield return new WaitForSeconds(0.25f);
                    foreach(var j in needDestory)
                    {
                        Destroy(j);
                    }
                    needDestory.Clear();
                    needDestory = null;
                }
                Destroy(go);
            }
        }
        yield break;
    }

    private void EnemyInit(GameObject go, Vector3 spawnLocalPosition, Weapon weapon, EntityAction[] entityActions)
    {
        if (go == null)
            return;
        go.transform.localPosition = spawnLocalPosition;
        go.transform.parent = null;
        var enemyEntity = go.GetComponent<GameEntity>();
        var enemy = go.GetComponent<Enemy>();
        if(weapon != null)
            enemy.weapon = weapon;
        enemy.wi = new Weapon.WeaponInterface(enemyEntity, enemy.weapon);
        enemy.actions = new List<EntityAction>();
        for(int i = 0;i < entityActions.Length;i ++)
        {
            enemy.actions.Add(entityActions[i]);
        }
        enemy.StartAction();
    }
}
