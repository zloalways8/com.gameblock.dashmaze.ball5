using System;
using Configs;
using GameCore.Services;
using Services;

namespace GameCore
{
    public class Loop
    {
        private readonly IAudioService _audioService;
        private readonly IInputService _inputService;
        private readonly ITimer _timer;
        private GameData _data;

        private readonly IMoveService _moveService;
        private IDrawService _drawService;
        private readonly Level _level;

        public event Action OnLevelCompleted;



        public Loop(IAudioService audioService, IInputService inputService, ITimer timer, GameConfig config,
            GameData data)
        {
            _audioService = audioService;
            _inputService = inputService;
            _timer = timer;
            _data = data;
            _moveService = new MoveService(_audioService);
            _level = new Level(new LevelFactory(config));
            _inputService.OnSwiped += OnSwiped;
            _moveService.OnMoveEnded += OnMoveEnded;
        }

        public void StartGame(int levelId)
        {
            _timer.Reset();
            _timer.Unpause();
            _level.LoadLevel(levelId);
            _inputService.Unpause();
        }

        public void EndGame()
        {
            _timer.Reset();
            _level.ClearLevel();
        }

        private void OnSwiped(SwipeDirection direction)
        {
            _inputService.Pause();
            _moveService.TryToMove(direction, _level.CurrentLevel);
        }

        private void OnMoveEnded()
        {
            if (_level.IsAllRoadMarked())
            {
                OnLevelCompleted?.Invoke();
            }
            else
            {
                _inputService.GameUnpause();
            }
        }
    }
}