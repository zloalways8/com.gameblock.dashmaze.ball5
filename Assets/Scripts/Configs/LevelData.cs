using GameCore;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "NewLevel", menuName = "Level Data")]

    public class LevelData : ScriptableObject
    {
        public LevelPrefab prefab;
        public Color drawColor;

    }
}