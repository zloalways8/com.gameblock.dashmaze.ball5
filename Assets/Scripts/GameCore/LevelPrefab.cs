using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    public class LevelPrefab : MonoBehaviour
    {
        public List<Road> roads;
        public Road startPoint;
        public GameObject ball;
        public Vector2Int ballPosition;
    }
}