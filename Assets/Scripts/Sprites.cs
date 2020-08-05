using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprites
{
    private static Sprite[] spritesArray;
    public static Sprite[] SpritesArray { 
        get
        {
            if (spritesArray == null)
                LoadSprites();
            return spritesArray; 
        } 
    }

    private static Dictionary<string, Sprite> spriteDictionary = new Dictionary<string, Sprite>();
    public static Dictionary<string, Sprite> SpriteDictionary { 
        get 
        {
            if (spritesArray == null)
                LoadSprites();
            return spriteDictionary; 
        }
    }

    private static void LoadSprites()
    {
        spritesArray = Resources.LoadAll<Sprite>("Pictures");
        foreach(var i in spritesArray)
        {
            spriteDictionary.Add(i.name, i);
        }
    }
}
