using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstLevel : Level
{
    public GameObject[] BulletPrefabs;

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
            if (preCoroutine != null)
                StopCoroutine(preCoroutine);
            StartCoroutine(EndFirstLevel(musicPlayer));
        }
    }

    private IEnumerator DoFirstLevel()
    {
        List<Object> needDestroy = new List<Object>();
        //0:00-0:02
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
                MoveVectorByTime moveVectorByTime = ActionMaker.MakeActionMoveVectorByTime(new Vector2(0, -12), 1f);
                EnemyInit(go, Vector3.zero, Weapons[0], new EntityAction[] { moveVectorByTime }, 1f);
                yield return new WaitForSeconds(1f);
            }
        }

        //0:03
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
                MoveVectorByTime moveVectorByTime = ActionMaker.MakeActionMoveVectorByTime(new Vector2(0f, -12f), 1f);
                EnemyInit(go, Vector3.zero, Weapons[1], new EntityAction[] { moveVectorByTime }, 1.24f);
                yield return new WaitForSeconds(1.24f);
            }
        }

        //0:04-0:10
        {
            EntityAction[] entityActionPre = new EntityAction[8];
            MoveAndShoot moveAndShoot = ActionMaker.MakeActionMoveAndShoot(Vector2.zero, 0.1f);
            MoveVectorByTime move = ActionMaker.MakeActionMoveVectorByTime(new Vector2(-3, 6), 0.25f);
            MoveVectorByTime mirrorXMove = ActionMaker.MakeActionMoveVectorByTime(new Vector2(-3, 6), 0.25f);
            mirrorXMove.MirrorX = true;
            MoveCircleByTime moveCircleByTime = ActionMaker.MakeActionMoveCircleByTime(new Vector2(-3f, 0f), 3f, 0.375f);
            MoveCircleByTime moveCircleByTimeIsClock = ActionMaker.MakeActionMoveCircleByTime(new Vector2(3f, 0f), 3f, 0.375f);
            moveCircleByTimeIsClock.IsClockDirect = true;

            MoveCircleByTime moveUpByCircle = ActionMaker.MakeActionMoveCircleByTime(new Vector2(3f, 6f), 3f, 0.375f);
            MoveCircleByTime moveUpByCircleIsClock = ActionMaker.MakeActionMoveCircleByTime(new Vector2(-3f, 6f), 3f, 0.375f);
            moveUpByCircleIsClock.IsClockDirect = true;

            MoveVectorByTime stay = ActionMaker.MakeActionMoveVectorByTime(new Vector2(0f, 0f), 0.25f);
            entityActionPre[0] = moveAndShoot;
            entityActionPre[1] = move;
            entityActionPre[2] = mirrorXMove;
            entityActionPre[3] = moveCircleByTime;
            entityActionPre[4] = moveCircleByTimeIsClock;
            entityActionPre[5] = moveUpByCircle;
            entityActionPre[6] = moveUpByCircleIsClock;
            entityActionPre[7] = stay;

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
                    float suicideTime = 0f;
                    if (i % 2 == 0)
                        spawnLocalPos = new Vector2(-3f, 0f);
                    else
                        spawnLocalPos = new Vector2(3f, 0f);

                    List<EntityAction> actions = new List<EntityAction>();
                    actions.Add(entityActionPre[0]);
                    suicideTime += 0.25f;
                    if (i != 14)
                    {
                        if (i % 2 == 0)
                            actions.Add(entityActionPre[2]);
                        else
                            actions.Add(entityActionPre[1]);
                        suicideTime += 0.25f;
                    }
                    else
                    {
                        actions.Add(entityActionPre[7]);
                        if (i % 2 == 0)
                            actions.Add(entityActionPre[2]);
                        else
                            actions.Add(entityActionPre[1]);
                        suicideTime += 0.5f;
                    }

                    EnemyInit(go, spawnLocalPos, Weapons[0], actions.ToArray(), suicideTime);
                    if (i == 14)
                    {
                        var goCir = GameObject.Instantiate(EnemyPrefabs[1], transform);
                        EntityAction[] entityActions = new EntityAction[] { entityActionPre[3], entityActionPre[6] };
                        EnemyInit(goCir, new Vector2(-3f, 0f), Weapons[1], entityActions, 0.75f);

                        goCir = GameObject.Instantiate(EnemyPrefabs[1], transform);
                        entityActions[0] = entityActionPre[4];
                        entityActions[1] = entityActionPre[5];
                        EnemyInit(goCir, new Vector2(3f, 0f), Weapons[1], entityActions, 0.75f);
                    }
                    yield return new WaitForSeconds(0.5f);
                    if (i == 14)
                    {
                        yield return new WaitForSeconds(0.25f);
                    }
                }
            }
        }
        //0:11-0:18
        for (int i = 0; i < 15; i++)
        {
            if (GameStatus.IsPauseGame())
            {
                i--;
                yield return 0;
            }
            else
            {
                int indexOfInstiate;
                if (i % 2 == 0)
                    indexOfInstiate = 0;
                else
                    indexOfInstiate = 1;
                if ((i / 2) % 2 != 0)
                {
                    GameObject go1 = GameObject.Instantiate(EnemyPrefabs[indexOfInstiate], transform);
                    Vector2 spawnPoint1 = new Vector2(-3f, 0);
                    Vector2 targetPoint1 = new Vector2(-3f, 4f);
                    MoveCircleByTime leftDown1 = ActionMaker.MakeActionMoveCircleByTime(targetPoint1, 1f, 0.25f);
                    MoveCircleByTime leftUp1 = ActionMaker.MakeActionMoveCircleByTime(new Vector2(spawnPoint1.x, 6f), 1f, 0.25f);
                    leftUp1.IsClockDirect = true;
                    EntityAction[] entityActions1 = new EntityAction[] { leftDown1, leftUp1 };

                    GameObject go2 = GameObject.Instantiate(EnemyPrefabs[indexOfInstiate], transform);
                    Vector2 spawnPoint2 = new Vector2(0f, 0f);
                    Vector2 targetVector = new Vector2(0f, -2f);
                    MoveVectorByTime straightDown = ActionMaker.MakeActionMoveVectorByTime(targetVector, 0.25f);
                    MoveVectorByTime straightUp = ActionMaker.MakeActionMoveVectorByTime(-targetVector, 0.25f);
                    EntityAction[] entityActions2 = new EntityAction[] { straightDown, straightUp };

                    GameObject go3 = GameObject.Instantiate(EnemyPrefabs[indexOfInstiate], transform);
                    Vector2 spawnPoint3 = new Vector2(3f, 0f);
                    Vector2 targetPoint3 = new Vector2(3f, 4f);
                    MoveCircleByTime rightDown3 = ActionMaker.MakeActionMoveCircleByTime(targetPoint3, 1f, 0.25f);
                    MoveCircleByTime rightUp3 = ActionMaker.MakeActionMoveCircleByTime(new Vector2(spawnPoint3.x, 6f), 1f, 0.25f);
                    rightDown3.IsClockDirect = true;
                    EntityAction[] entityActions3 = new EntityAction[] { rightDown3, rightUp3 };

                    EnemyInit(go1, spawnPoint1, Weapons[indexOfInstiate], entityActions1, 0.5f);
                    EnemyInit(go2, spawnPoint2, Weapons[indexOfInstiate], entityActions2, 0.5f);
                    EnemyInit(go3, spawnPoint3, Weapons[indexOfInstiate], entityActions3, 0.5f);
                }
                else
                {
                    GameObject go1 = GameObject.Instantiate(EnemyPrefabs[indexOfInstiate], transform);
                    Vector2 spawnPoint1 = new Vector2(-1.5f, 0);
                    Vector2 targetPoint1 = new Vector2(-1.5f, 2f);
                    MoveCircleByTime leftDown1 = ActionMaker.MakeActionMoveCircleByTime(targetPoint1, 2f, 0.25f);
                    MoveCircleByTime leftUp1 = ActionMaker.MakeActionMoveCircleByTime(new Vector2(spawnPoint1.x, 6f), 2f, 0.25f);
                    leftUp1.IsClockDirect = true;
                    EntityAction[] entityActions1 = new EntityAction[] { leftDown1, leftUp1 };

                    GameObject go2 = GameObject.Instantiate(EnemyPrefabs[indexOfInstiate], transform);
                    Vector2 spawnPoint2 = new Vector2(1.5f, 0f);
                    Vector2 targetPoint2 = new Vector2(1.5f, 2f);
                    MoveCircleByTime rightDown2 = ActionMaker.MakeActionMoveCircleByTime(targetPoint2, 2f, 0.25f);
                    MoveCircleByTime rightUp2 = ActionMaker.MakeActionMoveCircleByTime(new Vector2(spawnPoint2.x, 6f), 2f, 0.25f);
                    rightDown2.IsClockDirect = true;
                    EntityAction[] entityActions2 = new EntityAction[] { rightDown2, rightUp2 };

                    EnemyInit(go1, spawnPoint1, Weapons[indexOfInstiate], entityActions1, 0.5f);
                    EnemyInit(go2, spawnPoint2, Weapons[indexOfInstiate], entityActions2, 0.5f);
                }
                yield return new WaitForSeconds(0.5f);
            }
        }

        //0:18-0:21
        for (int i = 0; i < 1; i++)
        {
            if (GameStatus.IsPauseGame())
            {
                i--;
                yield return 0;
            }
            else
            {
                MoveVectorByTime stayTwoSeconds = ActionMaker.MakeActionMoveVectorByTime(Vector2.zero, 1f);

                Vector2 vector12 = new Vector2(0f, -3f);
                MoveVectorByTime straightDown12 = ActionMaker.MakeActionMoveVectorByTime(vector12, 0.5f);
                MoveVectorByTime straightUp12 = ActionMaker.MakeActionMoveVectorByTime(-vector12, 0.5f);
                EntityAction[] entityActions12 = new EntityAction[] { straightDown12, stayTwoSeconds, straightUp12 };

                Vector2 vector34 = new Vector2(5f, 0f);
                MoveVectorByTime straightLeft34 = ActionMaker.MakeActionMoveVectorByTime(-vector34, 0.5f);
                MoveVectorByTime straightRight34 = ActionMaker.MakeActionMoveVectorByTime(vector34, 0.5f);

                GameObject go1 = GameObject.Instantiate(EnemyPrefabs[1], transform);
                Vector2 spawnPoint1 = new Vector2(-2.5f, 0f);
                GameEntity go1Entity = go1.GetComponent<GameEntity>();

                GameObject go2 = GameObject.Instantiate(EnemyPrefabs[1], transform);
                Vector2 spawnPoint2 = -spawnPoint1;
                GameEntity go2Entity = go2.GetComponent<GameEntity>();

                GameObject go3 = GameObject.Instantiate(EnemyPrefabs[1], transform);
                go3.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, -90f));
                Vector2 spawnPoint3 = new Vector2(-10f, -5f);
                EntityAction[] entityActions3 = new EntityAction[] { straightRight34, stayTwoSeconds, straightLeft34 };
                GameEntity go3Entity = go3.GetComponent<GameEntity>();

                GameObject go4 = GameObject.Instantiate(EnemyPrefabs[1], transform);
                go4.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 90f));
                Vector2 spawnPoint4 = new Vector2(10f, -8f);
                EntityAction[] entityActions4 = new EntityAction[] { straightLeft34, stayTwoSeconds, straightRight34 };
                GameEntity go4Entity = go4.GetComponent<GameEntity>();

                GameEntity[] gameEntities = new GameEntity[] { go1Entity, go2Entity, go3Entity, go4Entity };
                foreach (var j in gameEntities)
                {
                    j.maxHP = 200;
                }
                EnemyInit(go1, spawnPoint1, Weapons[2], entityActions12, 2.5f);
                EnemyInit(go2, spawnPoint2, Weapons[2], entityActions12, 2.5f);
                EnemyInit(go3, spawnPoint3, Weapons[2], entityActions3, 2.5f);
                EnemyInit(go4, spawnPoint4, Weapons[2], entityActions4, 2.5f);
                yield return new WaitForSeconds(4f);
            }
        }

        //0:22-1:36
        {
            GameObject goParentPrefabs = new GameObject("GoParentPrefabs");
            needDestroy.Add(goParentPrefabs);
            var entity = goParentPrefabs.AddComponent<GameEntity>();
            entity.maxHP = 1;
            entity.currentHP = 1;
            goParentPrefabs.transform.parent = transform;
            goParentPrefabs.transform.localPosition = Vector2.zero;
            goParentPrefabs.transform.parent = null;
            goParentPrefabs.AddComponent<Suicide>().SuicideTime = 3f;

            List<EntityAction> entityActions = new List<EntityAction>();
            EntityAction entityAction = ActionMaker.MakeActionMoveVectorByTime(new Vector2(0f, -12f), 3f);
            entityActions.Add(entityAction);

            Vector2[] circleEnemySpawnPoint = new Vector2[4] { new Vector2(-0.8f, 0f), new Vector2(0.8f, 0f), new Vector2(0f, 0.8f), new Vector2(0f, -0.8f) };

            float spikerXOffset = 1f;
            float spikerYOffset = 0.2f;
            float calTime = 0f;

            Weapon[] weapons = new Weapon[2];
            weapons[0] = ScriptableObject.Instantiate(Weapons[3]);
            weapons[1] = ScriptableObject.Instantiate(Weapons[4]);
            if (GameDiffculty.diffculty == GameDiffculty.Diffculty.normal)
                weapons[1].baseLoadSpeed = 1f;
            if (GameDiffculty.diffculty == GameDiffculty.Diffculty.hard)
                weapons[1].baseLoadSpeed = 2f;
            for (int i = 0; i < 91; i++)
            {
                if (GameStatus.IsPauseGame())
                {
                    i--;
                    yield return 0;
                }
                else
                {
                    GameObject parent = GameObject.Instantiate(goParentPrefabs, transform);
                    parent.transform.localPosition = Vector3.zero;
                    parent.transform.parent = null;
                    float randomInstantiaPosX = Random.Range(-5f, 5.1f);
                    if (i % 2 == 0)
                    {
                        var ro = parent.AddComponent<StarRotate>();
                        ro.RotateEuler = 50f;
                        int shipCount = Random.Range(1, 5);
                        if (GameDiffculty.diffculty == GameDiffculty.Diffculty.easy)
                        {
                            if (shipCount >= 3)
                                shipCount = 2;
                        }
                        else if (GameDiffculty.diffculty == GameDiffculty.Diffculty.normal)
                        {
                            if (shipCount >= 4)
                                shipCount = 3;
                        }
                        else
                        {
                            if (shipCount < 2)
                                shipCount = 2;
                        }
                        for (int j = 0; j < shipCount; j++)
                        {
                            if (GameStatus.IsPauseGame())
                            {
                                j--;
                                yield return 0;
                            }
                            else
                            {
                                GameObject go = GameObject.Instantiate(EnemyPrefabs[1], parent.transform);
                                EnemyInit(go, circleEnemySpawnPoint[j], weapons[1], null, 0f);
                                go.transform.parent = parent.transform;
                                yield return 0;
                            }
                        }

                        if (i >= 16)
                        {
                            int randomNumber = Random.Range(1, 22);
                            if (((randomNumber % 5 == 0 || randomNumber % 7 == 0 || randomNumber % 11 == 0) && randomNumber % 3 != 0) || i == 16)
                            {
                                int laserShipCount = Random.Range(1, 4);
                                if (GameDiffculty.diffculty == GameDiffculty.Diffculty.easy)
                                {
                                    if (laserShipCount >= 2)
                                        laserShipCount = 1;
                                }
                                else if (GameDiffculty.diffculty == GameDiffculty.Diffculty.normal)
                                {
                                    if (laserShipCount >= 3)
                                        laserShipCount = 2;
                                }
                                for (int k = 0; k < laserShipCount; k++)
                                {
                                    if (GameStatus.IsPauseGame())
                                    {
                                        k--;
                                        yield return 0;
                                    }
                                    else
                                    {
                                        GameObject laserEnemy = GameObject.Instantiate(EnemyPrefabs[1], transform);
                                        Vector2 spawnLocalPoint = new Vector2(Random.Range(9.3f, 9.6f), Random.Range(-1.5f, -3f));
                                        int mirror = Random.Range(0, 2);
                                        if (mirror == 1)
                                            spawnLocalPoint.x = -spawnLocalPoint.x;
                                        MoveVectorByTime moveLeft = ActionMaker.MakeActionMoveVectorByTime(new Vector2(-19f, 0f), 2f);
                                        MoveVectorByTime moveRight = ActionMaker.MakeActionMoveVectorByTime(new Vector2(19f, 0f), 2f);
                                        EntityAction[] moveActions = new EntityAction[2] { moveLeft, moveRight };
                                        if (mirror == 1)
                                            moveActions = new EntityAction[] { moveRight, moveLeft };
                                        EnemyInit(laserEnemy, spawnLocalPoint, Weapons[5], moveActions, 8f);
                                        laserEnemy.GetComponent<Enemy>().ActionLoop(true);
                                    }
                                }
                            }

                        }
                    }
                    else if (i % 2 != 0)
                    {
                        int shipCount = Random.Range(1, 8);
                        Vector2 spawnPoint = Vector2.zero;
                        if (GameDiffculty.diffculty == GameDiffculty.Diffculty.easy)
                        {
                            if (shipCount >= 4)
                                shipCount = 3;
                        }
                        else if (GameDiffculty.diffculty == GameDiffculty.Diffculty.normal)
                        {
                            if (shipCount >= 6)
                                shipCount = 5;
                        }
                        else
                        {
                            if (shipCount < 3)
                                shipCount = 3;
                        }
                        for (int j = 0; j < shipCount; j++)
                        {
                            if (GameStatus.IsPauseGame())
                            {
                                j--;
                                yield return 0;
                            }
                            else
                            {
                                GameObject go = GameObject.Instantiate(EnemyPrefabs[0], parent.transform);
                                Vector2 spawn = spawnPoint;
                                if (j % 2 != 0)
                                    spawn.x = -spawn.x;
                                EnemyInit(go, spawn, weapons[0], null, 0f);
                                go.transform.parent = parent.transform;
                                if (j % 2 == 0)
                                {
                                    spawnPoint.x += spikerXOffset;
                                    spawnPoint.y += spikerYOffset;
                                }
                                yield return 0;
                            }
                        }
                    }
                    parent.transform.position = new Vector3(randomInstantiaPosX, parent.transform.position.y);
                    ActionList actionList = new ActionList(parent.GetComponent<GameEntity>(), entityActions);
                    actionList.Start();
                    parent.GetComponent<Suicide>().StartCountTime();
                    yield return new WaitForSeconds(0.8f);
                    calTime += 0.8f;

                }
                if (i == 14)
                {
                    yield return new WaitForSeconds(2.8f);
                }
            }
        }

        //1:37-1:51
        {
            CircleGun circleGunEvil = ScriptableObject.CreateInstance<CircleGun>();
            circleGunEvil.fullLoad = 1f;
            circleGunEvil.baseLoadSpeed = 5f;
            circleGunEvil.bulletVelocity = 5f;
            circleGunEvil.bulletDamage = 10f;
            circleGunEvil.bulletDuration = 8f;
            circleGunEvil.angleRate = 90f;
            circleGunEvil.angleOffset = 6f;
            GameObject circleBulletEvilPrefabs = GameObject.Instantiate(BulletPrefabs[0], transform);
            circleBulletEvilPrefabs.transform.localPosition = Vector2.zero;
            circleBulletEvilPrefabs.GetComponent<SpriteRenderer>().color = Color.red;
            circleGunEvil.ammunition = circleBulletEvilPrefabs.GetComponent<Bullet>();
            circleGunEvil.name = "6_CircleGunEvil";
            needDestroy.Add(circleBulletEvilPrefabs);
            Weapons.Add(circleGunEvil);
            for (int i = 0;i < 10;i ++)
            {
                if(GameStatus.IsPauseGame())
                {
                    i--;
                    yield return 0;
                }
                else
                {
                    GameObject evil = GameObject.Instantiate(EnemyPrefabs[1], transform);
                    var entity = evil.GetComponent<GameEntity>();
                    entity.maxHP = 1200f;
                    entity.currentHP = 1200f;
                    MoveVectorByTime move = ActionMaker.MakeActionMoveVectorByTime(new Vector2(0f, -12f), 6f);
                    float randomInstantiaPosX = Random.Range(-5f, 5.1f);
                    EnemyInit(evil, new Vector2(randomInstantiaPosX, 0f), circleGunEvil, new EntityAction[] { move }, 6.5f);
                    yield return new WaitForSeconds(5f);
                }
            }
        }
        foreach(var i in needDestroy)
        {
            if (i != null)
                Destroy(i);
        }
        yield break;
    }

    private void EnemyInit(GameObject go, Vector3 spawnLocalPosition, Weapon weapon, EntityAction[] entityActions, float suicideTime)
    {
        if (go == null)
            return;
        go.transform.localPosition = spawnLocalPosition;
        go.transform.parent = null;
        var suicide = go.AddComponent<Suicide>();
        suicide.SuicideTime = suicideTime;
        var enemyEntity = go.GetComponent<GameEntity>();
        var enemy = go.GetComponent<Enemy>();
        if (weapon != null)
            enemy.weapon = weapon;
        enemy.wi = new Weapon.WeaponInterface(enemyEntity, enemy.weapon);
        enemy.actions = new List<EntityAction>();
        if (entityActions != null)
        {
            for (int i = 0; i < entityActions.Length; i++)
            {
                enemy.actions.Add(entityActions[i]);
            }
            enemy.StartAction();
        }
        suicide.StartCountTime();
    }

    private IEnumerator EndFirstLevel(AudioSource musicPlayer)
    {
        while(musicPlayer.time <= 113.4f)
        {
            yield return 0;
        }
        musicPlayer.Pause();
        yield break;
    }
}
