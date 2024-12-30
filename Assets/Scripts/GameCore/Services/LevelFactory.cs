using Configs;
using UnityEngine;

namespace GameCore.Services
{
    public class LevelFactory : ILevelFactory
    {
        private GameConfig _config;

        public LevelFactory(GameConfig config)
        {
            _config = config;
        }

        public LevelPrefab CreateLevel(int id)
        {
            var level = Object.Instantiate(_config.levels[id].prefab);
            var ball = Object.Instantiate(_config.ballPrefab, new Vector3(level.startPoint.transform.position.x, level.startPoint.transform.position.y, -1f),
                Quaternion.identity);
            level.ball = ball;
            for (int i = 0; i < level.roads.Count; i++)
            {
                level.roads[i].Init(_config.levels[id].drawColor);
            }
            
            level.startPoint.Mark();

            level.ballPosition = new Vector2Int(level.startPoint.x, level.startPoint.y);
            
            return level;
        }
    }

    public interface ILevelFactory
    {
        LevelPrefab CreateLevel(int id);
    }
}