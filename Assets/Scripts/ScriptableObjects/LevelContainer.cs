using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(order = 52, fileName = "LevelContainer", menuName = "Tools/LevelContainer")]
public class LevelContainer : ScriptableObject
{
    public List<LevelData> levelContainer;

    public LevelData NextLevel(LevelData currentLevel)
    {
        int index;
        if (currentLevel == null)
        {
            index = 0;
        }
        else
        {
            index = levelContainer.IndexOf(currentLevel);
        }

        index++;
        if (index >= levelContainer.Count)
        {
            index = 0;
        }

        return levelContainer[index];
    }
}
