using GameCore.Services;
using UnityEngine;

namespace GameCore
{
    public class Level
    {
        private readonly ILevelFactory _factory;
        private LevelPrefab currentLevel;
        public LevelPrefab CurrentLevel => currentLevel;

        private Vector2Int ballPosition;


        public Level(ILevelFactory factory)
        {
            _factory = factory;
        }

        public void LoadLevel(int id)
        {
            currentLevel = _factory.CreateLevel(id);
        }

        public void ClearLevel()
        {
            if (currentLevel != null)
            {
                if (currentLevel.ball != null)
                {
                    Object.Destroy(currentLevel.ball.gameObject);
                }
                Object.Destroy(currentLevel.gameObject);
                
                currentLevel = null;
            }
            
        }

        public bool IsAllRoadMarked()
        {
            int drawed = 0;
            for (int i = 0; i < currentLevel.roads.Count; i++)
            {
                if (currentLevel.roads[i].isMarked)
                {
                    drawed++;
                }
            }

            return drawed == currentLevel.roads.Count;
        }
    }
}