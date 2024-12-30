using System;
using System.Collections.Generic;
using GameCore;
using UnityEngine;
using UnityEngine.Serialization;

namespace Configs
{
    [CreateAssetMenu(menuName = "Configs/GameConfig", fileName = "GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [FormerlySerializedAs("_levels")] public List<LevelData> levels;
        public GameObject ballPrefab;
    }
}