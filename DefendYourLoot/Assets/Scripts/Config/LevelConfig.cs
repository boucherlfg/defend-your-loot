using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Config
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Felix/LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        public float spawnInterval = 3;
        public List<GameObject> heroes;
    }
}