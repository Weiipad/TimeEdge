using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject bullet;

    public int load;
    public int finishLoading;
    public float speed;

    private BulletGenerator launcher;

    // Start is called before the first frame update
    void Start()
    {
        var launcherPrefab = Resources.Load("Prefabs/BulletGenerator") as GameObject;
        var launcherObj = Instantiate(launcherPrefab, transform);
        launcher = launcherObj.GetComponent<BulletGenerator>();
        launcher.bullet = bullet;
    }

    // Update is called once per frame
    void Update()
    {
        if (load >= finishLoading)
        {

            if (Input.GetKey(KeyCode.Mouse0))
            {
                launcher.Shoot();
                load = 0;
            }
        } 
        else
        {
            load += 1;
        }
    }

    
}
