using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityScript.Macros;

[CreateAssetMenu(fileName = "NewVShapeGun", menuName = "Time Edge/Weapon/VShapeGun")]
public class VShapeGun : Weapon
{
    public enum VShapeDirect
    {
        up,
        down,
        left,
        right
    }

    /// <summary>
    /// V字形的层数
    /// </summary>
    public int bulletLevel = 5;

    /// <summary>
    /// 基础层间距
    /// </summary>
    public float levelSpacingBase = 0.2f;
    
    /// <summary>
    /// 层间距差值
    /// </summary>
    public float levelSpacingOffset = 0.0f;

    /// <summary>
    /// 每层两端端点间距
    /// </summary>
    public float endSpacingBase = 0.4f;

    /// <summary>
    /// 端点间距差值
    /// </summary>
    public float endSpacingOffset = 0.1f;

    /// <summary>
    /// V字指向
    /// </summary>
    public VShapeDirect direct = VShapeDirect.down;

    private bool isGen = false;
    protected override void TryShoot(WeaponInterface wi)
    {
        if(wi.load >= wi.fullLoad && !isGen)
        {
            isGen = true;
            GenBullet(wi, wi.owner.transform.position);
            Vector2 genPosBase = wi.owner.transform.position;
            Vector2 genPos = Vector2.zero;
            float tLevelSpacingBase = levelSpacingBase;
            float tEndSpacingBase = endSpacingBase;
            for (int i = 0;i < bulletLevel - 1;i ++)
            {
                //first bullet
                switch(direct)
                {
                    case VShapeDirect.up:
                        genPos.x = genPosBase.x + tEndSpacingBase / 2.0f;
                        genPos.y = genPosBase.y - tLevelSpacingBase;
                        break;
                    case VShapeDirect.down:
                        genPos.x = genPosBase.x + tEndSpacingBase / 2.0f;
                        genPos.y = genPosBase.y + tLevelSpacingBase;
                        break;
                    case VShapeDirect.left:
                        genPos.y = genPosBase.y + tEndSpacingBase / 2.0f;
                        genPos.x = genPosBase.x + tLevelSpacingBase;
                        break;
                    case VShapeDirect.right:
                        genPos.y = genPosBase.y + tEndSpacingBase / 2.0f;
                        genPos.x = genPosBase.x - tLevelSpacingBase;
                        break;
                }
                
                GenBullet(wi, genPos);
                switch (direct)
                {
                    case VShapeDirect.down:
                        genPos.x = genPosBase.x + tEndSpacingBase / 2.0f;
                        genPos.y = genPosBase.y + tLevelSpacingBase;
                        break;
                    case VShapeDirect.left:
                        genPos.y = genPosBase.y + tEndSpacingBase / 2.0f;
                        genPos.x = genPosBase.x + tLevelSpacingBase;
                        break;
                    case VShapeDirect.right:
                        genPos.y = genPosBase.y + tEndSpacingBase / 2.0f;
                        genPos.x = genPosBase.x - tLevelSpacingBase;
                        break;
                }

                //second bullet
                switch (direct)
                {
                    case VShapeDirect.up:
                        genPos.x = genPosBase.x - tEndSpacingBase / 2.0f;
                        genPos.y = genPosBase.y - tLevelSpacingBase;
                        break;
                    case VShapeDirect.down:
                        genPos.x = genPosBase.x - tEndSpacingBase / 2.0f;
                        genPos.y = genPosBase.y + tLevelSpacingBase;
                        break;
                    case VShapeDirect.left:
                        genPos.y = genPosBase.y - tEndSpacingBase / 2.0f;
                        genPos.x = genPosBase.x + tLevelSpacingBase;
                        break;
                    case VShapeDirect.right:
                        genPos.y = genPosBase.y - tEndSpacingBase / 2.0f;
                        genPos.x = genPosBase.x - tLevelSpacingBase;
                        break;
                }

                GenBullet(wi, genPos);
                
                switch(direct)
                {
                    case VShapeDirect.up:
                        genPosBase.y -= tLevelSpacingBase;
                        break;
                    case VShapeDirect.down:
                        genPosBase.y += tLevelSpacingBase;
                        break;
                    case VShapeDirect.left:
                        genPosBase.x += tLevelSpacingBase;
                        break;
                    case VShapeDirect.right:
                        genPosBase.x -= tLevelSpacingBase;
                        break;
                }
                tLevelSpacingBase += levelSpacingOffset;
                tEndSpacingBase += endSpacingOffset;
            }
            isGen = false;
            wi.load = 0;
        }
    }

    private Bullet GenBullet(WeaponInterface wi, Vector2 genPos)
    {
        Bullet bullet = GameObject.Instantiate(ammunition, genPos, wi.owner.transform.rotation);
        ModifyRotation(bullet, wi);
        bullet.damage = bulletDamage * wi.owner.damageRate;
        bullet.velocity = bulletVelocity;
        bullet.duration = bulletDuration;
        return bullet;
    }

    private void ModifyRotation(Bullet bullet, WeaponInterface wi)
    {
        Vector3 parentEuler = wi.owner.transform.rotation.eulerAngles;
        switch(direct)
        {
            case VShapeDirect.up:
                bullet.transform.rotation = Quaternion.Euler(new Vector3(parentEuler.x, parentEuler.y, 0.0f));
                break;
            case VShapeDirect.down:
                bullet.transform.rotation = Quaternion.Euler(new Vector3(parentEuler.x, parentEuler.y, 180.0f));
                break;
            case VShapeDirect.left:
                bullet.transform.rotation = Quaternion.Euler(new Vector3(parentEuler.x, parentEuler.y, 90.0f));
                break;
            case VShapeDirect.right:
                bullet.transform.rotation = Quaternion.Euler(new Vector3(parentEuler.x, parentEuler.y, -90.0f));
                break;
        }
    }
}
