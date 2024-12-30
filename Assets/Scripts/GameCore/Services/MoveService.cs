using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Services;
using UnityEngine;

namespace GameCore.Services
{
    public class MoveService : IMoveService
    {
        private readonly IAudioService _audioService;
        public event Action OnMoveEnded;
        
        public MoveService(IAudioService audioService)
        {
            _audioService = audioService;
        }
        
        public void TryToMove(SwipeDirection direction, LevelPrefab levelPrefab)
        {
            
            List<Road> path = new List<Road>();
            bool hasPath = true;

            int index = 0;
            
            switch (direction)
            {
                case SwipeDirection.Up:
                    index = levelPrefab.ballPosition.y;
                    
                    while (hasPath)
                    {
                        var road = levelPrefab.roads
                            .FirstOrDefault(r => r.x == levelPrefab.ballPosition.x && r.y == index + 1);

                        if (road != null)
                        {
                            path.Add(road);
                            index++;
                        }
                        else
                        {
                            hasPath = false;
                        }

                    }
                    
                    break;
                case SwipeDirection.Down:
                    
                    index = levelPrefab.ballPosition.y;
                    
                    while (hasPath)
                    {
                        var road = levelPrefab.roads
                            .FirstOrDefault(r => r.x == levelPrefab.ballPosition.x && r.y == index - 1);

                        if (road != null)
                        {
                            path.Add(road);
                            index--;
                        }
                        else
                        {
                            hasPath = false;
                        }

                    }
                    break;
                case SwipeDirection.Right:
                    
                    index = levelPrefab.ballPosition.x;
                    
                    while (hasPath)
                    {
                        var road = levelPrefab.roads
                            .FirstOrDefault(r => r.x == index + 1 && r.y == levelPrefab.ballPosition.y);

                        if (road != null)
                        {
                            path.Add(road);
                            index++;
                        }
                        else
                        {
                            hasPath = false;
                        }

                    }
                    break;
                case SwipeDirection.Left:
                    
                    index = levelPrefab.ballPosition.x;
                    
                    while (hasPath)
                    {
                        var road = levelPrefab.roads
                            .FirstOrDefault(r => r.x == index - 1 && r.y == levelPrefab.ballPosition.y);

                        if (road != null)
                        {
                            path.Add(road);
                            index--;
                        }
                        else
                        {
                            hasPath = false;
                        }

                    }
                    break;
            }

            if (path.Count > 0)
            {
                Move(path, levelPrefab);
            }
            else
            {
                OnMoveEnded?.Invoke();
            }
        }

        private void Move(List<Road> pth, LevelPrefab lvl)
        {
            Sequence moveSequence = DOTween.Sequence();

            for (int i = 0; i < pth.Count; i++)
            {
                int c = i;
                moveSequence
                    .Append(lvl.ball.transform.DOMove(
                        new Vector3(pth[c].transform.position.x, pth[c].transform.position.y,
                            lvl.ball.transform.position.z), 0.1f).OnComplete(() =>
                    {
                        _audioService.PlaySound(1);
                        pth[c].Mark();
                    }));

            }

            moveSequence.Play().OnComplete(() =>
            {
                lvl.ballPosition.x = pth[pth.Count - 1].x;
                lvl.ballPosition.y = pth[pth.Count - 1].y;
                OnMoveEnded?.Invoke();
            });
        }
    }

    public interface IMoveService
    {
        event Action OnMoveEnded;
        void TryToMove(SwipeDirection direction, LevelPrefab levelPrefab);
    }
}