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
        bossEnemy.actions = new List<EntityAction>() { moveCircleLeftDown, moveCircleRightDown, moveCircleRightUp, moveCircleLeftUp };
        bossEnemy.ActionLoop(true);
        bossEnemy.StartAction();
    }
}
