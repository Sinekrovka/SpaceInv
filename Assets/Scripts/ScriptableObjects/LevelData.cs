using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(order = 51, fileName = "LevelData", menuName = "Tools/Level")]
public class LevelData : ScriptableObject
{
    public List<LevelEnemy> _enemies;
    
    [Serializable]
    public struct LevelEnemy
    {
        public int count;
        public GameObject prefab;
    }
}
