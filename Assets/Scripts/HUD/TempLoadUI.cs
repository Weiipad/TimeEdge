using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempLoadUI : MonoBehaviour
{
    public Text text;
    public Player player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = string.Format("{0:N2}", player.wi.load) + $"/{player.wi.fullLoad}";
    }
}
