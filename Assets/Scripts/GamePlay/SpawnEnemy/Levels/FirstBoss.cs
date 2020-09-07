using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;

public class FirstBoss : Level
{
    private enum BossLevel
    {
        none,
        first,
        second
    }
    private GameObject boss;
    private GameEntity bossEntity;
    private Enemy bossEnemy;
    private Enemy bossLeftGun;
    private Enemy bossRightGun;

    private Collider2D bossCollider;

    private AudioSource musicPlayer;

    private BossLevel bossLevel = BossLevel.none;
    public override void StartLevel(LevelList levelList)
    {
        StartCoroutine(WaitTimeToStartFightBoss());
    }

    private void LevelInit()
    {
        boss = EnemyPrefabs[0];
        boss.SetActive(true);
        bossEnemy = boss.GetComponent<Enemy>();
        bossEntity = boss.GetComponent<GameEntity>();
        bossLeftGun = boss.transform.GetChild(0)?.GetComponent<Enemy>();
        bossRightGun = boss.transform.GetChild(1)?.GetComponent<Enemy>();
        musicPlayer = GameObject.Find("AudioGroup").transform.GetChild(0).GetComponent<AudioSource>();
        musicPlayer.time = 0.0f;
        musicPlayer.clip = Musics[0];
    }

    IEnumerator WaitTimeToStartFightBoss()
    {
        float current = 0.0f;
        while(current <= 3.0f)
        {
            if (!GameStatus.IsPauseGame())
            {
                current += 0.02f;
                yield return new WaitForSeconds(0.02f);
            }
            else
                yield return 0;
        }
        LevelInit();
        MoveVectorByTime strightDown = ActionMaker.MakeActionMoveVectorByTime(new Vector2(0.0f, -2.0f), 6f);
        strightDown.AfterActionDelegate += TheBossShowUp;
        strightDown.IsStopSwitch = true;
        bossEnemy.actions.Add(strightDown);
        bossEnemy.StartAction();
        musicPlayer.loop = true;
        musicPlayer.Play();
    }

    public override void EndLevel()
    {

    }

    private void Update()
    {
        if(bossLevel == BossLevel.first && bossEntity.currentHP/bossEntity.maxHP <= 2.0f/3.0f)
        {
            BeforeTurnToSecondLevel();
        }
    }

    private void TheBossShowUp()
    {
        bossLevel = BossLevel.first;
        bossCollider = boss.GetComponent<BoxCollider2D>();
        bossCollider.enabled = true;
        if (bossLeftGun != null)
            bossLeftGun.EquipWeapon(Weapons[0]);
        if (bossRightGun != null)
            bossRightGun.EquipWeapon(Weapons[0]);
        MoveCircleByTime moveCircleLeftDown = ActionMaker.MakeActionMoveCircleByTime(new Vector2(-0.5f, 3.6f), 0.5f, 0.2f);
        MoveCircleByTime moveCircleRightDown = ActionMaker.MakeActionMoveCircleByTime(new Vector2(0.0f, 3.1f), 0.5f, 0.2f);
        MoveCircleByTime moveCircleRightUp = ActionMaker.MakeActionMoveCircleByTime(new Vector2(0.5f, 3.6f), 0.5f, 0.2f);
        MoveCircleByTime moveCircleLeftUp = ActionMaker.MakeActionMoveCircleByTime(new Vector2(0.0f, 4.1f), 0.5f, 0.2f);
        List<EntityAction> actions = new List<EntityAction>();
        for (int i = 0; i < 4; i++)
        {
            actions.Add(moveCircleLeftDown);
            actions.Add(moveCircleRightDown);
            actions.Add(moveCircleRightUp);
            actions.Add(moveCircleLeftUp);
        }
        MoveVectorByTime moveLeft = ActionMaker.MakeActionMoveVectorByTime(new Vector2(-4.0f, 0.0f), 1.0f);
        MoveVectorByTime moveRight = ActionMaker.MakeActionMoveVectorByTime(new Vector2(8.0f, 0.0f), 2.0f);
        MoveVectorByTime moveLeftBack = ActionMaker.MakeActionMoveVectorByTime(new Vector2(-4.0f, 0.0f), 1.0f);
        moveLeftBack.AfterActionDelegate += () => { boss.transform.position = new Vector3(0.0f, 4.1f, 0.0f); };
        MoveVectorByTime stay = ActionMaker.MakeActionMoveVectorByTime(Vector2.zero, 1.0f);
        stay.BeforeActionDelegate += () =>
        {
            bossLeftGun.RemoveWeapon();
            bossRightGun.RemoveWeapon();
        };
        MoveVectorByTime strightDownOutOfScreen = ActionMaker.MakeActionMoveVectorByTime(new Vector2(0.0f, -10.0f), 2.0f);
        strightDownOutOfScreen.BeforeActionDelegate += () =>
        {
            bossEnemy.EquipWeapon(Weapons[1]);
            bossLeftGun.RemoveWeapon();
            bossRightGun.RemoveWeapon();
        };
        strightDownOutOfScreen.AfterActionDelegate += () =>
        {
            bossEnemy.RemoveWeapon();
            bossLeftGun.RemoveWeapon();
            bossRightGun.RemoveWeapon();
        };

        MoveCircleByTime backSpawnPos = ActionMaker.MakeActionMoveCircleByTime(new Vector2(0.0f, 6.1f), 6.1f, 2.0f);
        MoveVectorByTime strightDown = ActionMaker.MakeActionMoveVectorByTime(new Vector2(0.0f, -2.0f), 3.0f);
        strightDown.AfterActionDelegate += () =>
        {
            bossLeftGun.EquipWeapon(Weapons[0]);
            bossRightGun.EquipWeapon(Weapons[0]);
        };
        actions.Add(moveLeft);
        actions.Add(moveRight);
        actions.Add(moveLeftBack);
        actions.Add(stay);
        actions.Add(strightDownOutOfScreen);
        actions.Add(backSpawnPos);
        actions.Add(strightDown);
        bossEnemy.actions = actions;
        bossEnemy.ActionLoop(true);
        bossEnemy.StartAction();
    }

    private void BeforeTurnToSecondLevel()
    {
        bossLevel = BossLevel.second;
        boss.GetComponent<Collider2D>().enabled = false;
        bossEnemy.RemoveWeapon();
        bossLeftGun.RemoveWeapon();
        if (bossLeftGun.transform.childCount > 0)
            Destroy(bossLeftGun.transform.GetChild(0));
        bossRightGun.RemoveWeapon();
        if (bossRightGun.transform.childCount > 0)
            Destroy(bossRightGun.transform.GetChild(0));
        Vector2 newPos = new Vector2(0.0f, 3.5f);
        MoveVectorByTime moveToNewPoint = ActionMaker.MakeActionMoveVectorByTime(newPos - (Vector2)boss.transform.position, 2.0f);
        moveToNewPoint.BeforeActionDelegate += () =>
        {
            bossEnemy.RemoveWeapon();
            bossEnemy.RemoveWeapon();
            bossLeftGun.RemoveWeapon();
        };
        moveToNewPoint.AfterActionDelegate += () => { boss.GetComponent<Collider2D>().enabled = true; };
        moveToNewPoint.AfterActionDelegate += () => { boss.transform.position = newPos; };
        moveToNewPoint.AfterActionDelegate += () => { SecondLevelOfBoss(); };
        moveToNewPoint.IsStopSwitch = true;
        List<EntityAction> entityActions = new List<EntityAction>() { moveToNewPoint };
        bossEnemy.actions = entityActions;
        bossEnemy.ActionLoop(false);
        bossEnemy.StopAllCoroutines();
        bossEnemy.StartAction();
    }

    private void SecondLevelOfBoss()
    {
        //cirle move
        MoveCircleByTime moveLeftUnClockStart = ActionMaker.MakeActionMoveCircleByTime(new Vector2(-2.0f, 3.5f), 1f, 1.0f);
        MoveCircleByTime moveLeftUnClock = ActionMaker.MakeActionMoveCircleByTime(new Vector2(-2.0f, 3.5f), 1f, 1.0f);
        moveLeftUnClockStart.BeforeActionDelegate += () =>
        {
            bossEnemy.EquipWeapon(Weapons[2]);
            bossLeftGun.RemoveWeapon();
            bossRightGun.RemoveWeapon();
        };
        MoveCircleByTime moveRightUnClock = ActionMaker.MakeActionMoveCircleByTime(new Vector2(0.0f, 3.5f), 1f, 1.0f);
        MoveCircleByTime moveRightClock = ActionMaker.MakeActionMoveCircleByTime(new Vector2(2.0f, 3.5f), 1f, 1.0f);
        moveRightClock.IsClockDirect = true;
        MoveCircleByTime moveLeftClock = ActionMaker.MakeActionMoveCircleByTime(new Vector2(0.0f, 3.5f), 1f, 1.0f);
        moveLeftClock.IsClockDirect = true;
        //end

        //line move
        MoveVectorByTime moveLeftStart = ActionMaker.MakeActionMoveVectorByTime(new Vector2(-4.0f, 0.0f), 2.0f);
        MoveVectorByTime moveRightLoop = ActionMaker.MakeActionMoveVectorByTime(new Vector2(8.0f, 0.0f), 4.0f);
        MoveVectorByTime moveLeftLoop = ActionMaker.MakeActionMoveVectorByTime(new Vector2(-8.0f, 0.0f), 4.0f);
        MoveVectorByTime moveRightAndEquipLaser = ActionMaker.MakeActionMoveVectorByTime(new Vector2(8.0f, 0.0f), 4.0f);
        moveRightAndEquipLaser.BeforeActionDelegate += () =>
        {
            bossLeftGun.EquipWeapon(Weapons[3]);
            bossRightGun.EquipWeapon(Weapons[3]);
        };
        MoveVectorByTime moveLeftAndRemoveLaser = ActionMaker.MakeActionMoveVectorByTime(new Vector2(-8.0f, 0.0f), 4.0f);
        moveLeftAndRemoveLaser.AfterActionDelegate += () =>
        {
            bossLeftGun.RemoveWeapon();
            bossRightGun.RemoveWeapon();
            Destroy(bossLeftGun.transform.GetChild(0).gameObject);
            Destroy(bossRightGun.transform.GetChild(0).gameObject);
        };
        MoveVectorByTime moveBackToPoint = ActionMaker.MakeActionMoveVectorByTime(new Vector2(4.0f, 0.0f), 2.0f);
        //end

        //stright down and shot and 'S' shape back
        MoveVectorByTime strightDown = ActionMaker.MakeActionMoveVectorByTime(new Vector2(0.0f, -7.0f), 2.0f);
        strightDown.BeforeActionDelegate += () =>
        {
            boss.transform.position = new Vector3(0.0f, 3.5f, 0.0f);
            bossLeftGun.EquipWeapon(Weapons[4]);
            bossRightGun.EquipWeapon(Weapons[5]);
        };

        MoveCircleByTime moveUpRight = ActionMaker.MakeActionMoveCircleByTime(new Vector2(0.0f, 0.0f), 3.5f/2.0f, 2.0f);
        moveUpRight.BeforeActionDelegate += () =>
        {
            boss.transform.position = new Vector3(0.0f, -3.5f, 0.0f);
            bossEnemy.RemoveWeapon();
            bossLeftGun.EquipWeapon(Weapons[4]);
            bossRightGun.EquipWeapon(Weapons[5]);
        };
        MoveCircleByTime moveUpLeft = ActionMaker.MakeActionMoveCircleByTime(new Vector2(0.0f, 3.5f), 3.5f/2.0f, 2.0f);
        moveUpLeft.AfterActionDelegate += () =>
        {
            bossLeftGun.RemoveWeapon();
            bossRightGun.RemoveWeapon();
        };
        moveUpLeft.IsClockDirect = true;
        //end

        //fast move and shot 'V' bullet when boss stay
        //move to point (2.1f, 3.5f)
        MoveVectorByTime moveToPos1 = ActionMaker.MakeActionMoveVectorByTime(new Vector2(2.1f, 0.0f), 0.2f);
        moveToPos1.AfterActionDelegate += () => { bossEnemy.EquipWeapon(Weapons[6]); };
        //move to point (-3.5f, 3.5f)
        MoveVectorByTime moveToPos2 = ActionMaker.MakeActionMoveVectorByTime(new Vector2(-5.6f, 0.0f), 0.2f);
        moveToPos2.BeforeActionDelegate += () => { bossEnemy.RemoveWeapon(); };
        moveToPos2.AfterActionDelegate += () => { bossEnemy.EquipWeapon(Weapons[6]); };
        //move to point (-2.0f, 3.5f)
        MoveVectorByTime moveToPos3 = ActionMaker.MakeActionMoveVectorByTime(new Vector2(1.5f, 0.0f), 0.2f);
        moveToPos3.BeforeActionDelegate += () => { bossEnemy.RemoveWeapon(); };
        moveToPos3.AfterActionDelegate += () => { bossEnemy.EquipWeapon(Weapons[6]); };
        //move to point (-3.0f, 3.5f)
        MoveVectorByTime moveToPos4 = ActionMaker.MakeActionMoveVectorByTime(new Vector2(-1.0f, 0.0f), 0.2f);
        moveToPos4.BeforeActionDelegate += () => { bossEnemy.RemoveWeapon(); };
        moveToPos4.AfterActionDelegate += () => { bossEnemy.EquipWeapon(Weapons[6]); };
        //move to point (2.0f, 3.5f)
        MoveVectorByTime moveToPos5 = ActionMaker.MakeActionMoveVectorByTime(new Vector2(5.0f, 0.0f), 0.2f);
        moveToPos5.BeforeActionDelegate += () => { bossEnemy.RemoveWeapon(); };
        moveToPos5.AfterActionDelegate += () => { bossEnemy.EquipWeapon(Weapons[6]); };
        //short stay
        MoveVectorByTime stayShortTime = ActionMaker.MakeActionMoveVectorByTime(Vector2.zero, 0.2f);
        //back
        MoveVectorByTime moveBack_fastMove = ActionMaker.MakeActionMoveVectorByTime(new Vector2(-2.5f, 0.0f), 0.5f);
        moveBack_fastMove.BeforeActionDelegate += () => { bossEnemy.RemoveWeapon(); };
        moveBack_fastMove.AfterActionDelegate += () => { bossEnemy.transform.position = new Vector3(0.0f, 3.5f, 0.0f); };
        //end

        List<EntityAction> actions = new List<EntityAction>();
        //add circle move
        for (int i = 0; i < 3; i++)
        { 
            if(i == 0)
                actions.Add(moveLeftUnClockStart);
            else
                actions.Add(moveLeftUnClock);
            actions.Add(moveRightUnClock);
            actions.Add(moveRightClock);
            actions.Add(moveLeftClock);
        }
        //end

        //and line move
        actions.Add(moveLeftStart);
        for(int i = 0;i < 2;i ++)
        {
            actions.Add(moveRightLoop);
            actions.Add(moveLeftLoop);
        }
        actions.Add(moveRightAndEquipLaser);
        actions.Add(moveLeftLoop);
        actions.Add(moveRightLoop);
        actions.Add(moveLeftAndRemoveLaser);
        actions.Add(moveBackToPoint);
        //end

        actions.Add(strightDown);
        actions.Add(moveUpRight);
        actions.Add(moveUpLeft);

        actions.Add(moveToPos1);
        actions.Add(stayShortTime);
        actions.Add(moveToPos2);
        actions.Add(stayShortTime);
        actions.Add(moveToPos3);
        actions.Add(stayShortTime);
        actions.Add(moveToPos4);
        actions.Add(stayShortTime);
        actions.Add(moveToPos5);
        actions.Add(stayShortTime);
        actions.Add(moveBack_fastMove);

        bossEnemy.actions = actions;
        bossEnemy.ActionLoop(true);
        bossEnemy.StartAction();
    }
}
