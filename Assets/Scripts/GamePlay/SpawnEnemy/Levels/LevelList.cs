using UnityEngine;
using System.Collections.Generic;

public class LevelList
{
    public LevelList() { }
    public LevelList(List<Level> levels) 
    {
        this.levels = levels;
    }

    public List<Level> levels;

    public void StartLevel()
    {
        if(levels.Count > 0)
            levels[0].StartLevel(this);
    }

    public void SwitchLevel(int index)
    {
        if (index < levels.Count)
            levels[index].StartLevel(this);
        else
            EndLevel();
    }

    public void EndLevel()
    {

    }
}
