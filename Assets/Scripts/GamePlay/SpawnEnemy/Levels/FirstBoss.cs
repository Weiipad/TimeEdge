using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBoss : Level
{
    private GameObject boss;
    private Enemy bossEnemy;
    private Enemy bossLeftGun;
    private Enemy bossRightGun;

    private Collider2D bossCollider;

    private AudioSource musicPlayer;

    public override void StartLevel(LevelList levelList)
    {
        boss = EnemyPrefabs[0];
        boss.SetActive(true);
        bossLeftGun = boss.transform.GetChild(0)?.GetComponent<Enemy>();
        bossRightGun = boss.transform.GetChild(1)?.GetComponent<Enemy>();
        MoveVectorByTime strightDown = ActionMaker.MakeActionMoveVectorByTime(new Vector2(0.0f, -2.0f), 6f);
        strightDown.AfterActionDelegate += TheBossShowUp;
        strightDown.IsStopSwitch = true;
        bossEnemy = boss.GetComponent<Enemy>();
        bossEnemy.actions.Add(strightDown);
        bossEnemy.StartAction();
        musicPlayer = GameObject.Find("AudioGroup").transform.GetChild(0).GetComponent<AudioSource>();
        musicPlayer.clip = Musics[0];
        musicPlayer.Play();
    }

    public override void EndLevel()
    {

    }

    private void StopAllFire()
    {
        bossEnemy.weapon = null;
        bossLeftGun.weapon = null;
        bossRightGun.weapon = null;
    }

    private void TheBossShowUp()
    {
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
}
