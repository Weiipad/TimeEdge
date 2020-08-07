using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShockBombNumber : MonoBehaviour
{
    private Player player;
    private int preCount;
    private int curCount;

    private Text text;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        text = gameObject.transform.GetChild(0).GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        curCount = player.avaliableShock;
        if(preCount != curCount)
        {
            preCount = curCount;
            text.text = " x " + curCount.ToString();
        }
        else
        {
            if (text.text == null || text.text == "")
            {
                text.text = " x " + curCount.ToString();
            }
        }
    }
}
