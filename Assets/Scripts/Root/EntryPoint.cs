using System.Collections.Generic;
using Configs;
using GameCore;
using GameCore.Services;
using Services;
using Services.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Screen = Services.UI.Screen;

namespace Root
{
    public class EntryPoint : MonoBehaviour
    {
        public List<Screen> screens;
        public AudioSource music;
        public AudioSource sound;
        public Sprite[] sprites;
        public Button[] soundButtons;
        public TextMeshProUGUI timer;

        private IConfigLoader _configLoader;
        private IUiService _uiService;
        private IAudioService _audioService;
        private IInputService _inputService;
        private GameData _gameData;
        private Loop _loop;

        private ITimer _timer;
        private AdsService _adsService;

        private void Start()
        {
            _adsService = new AdsService();
            _adsService.Init();
            _gameData = new GameData();
            _configLoader = new ConfigLoader(); 
            _inputService = new InputService();
            _uiService = new UiService(screens);
            foreach (var s in screens)
            {
                s.Init(_gameData);
                s.BindButtons(this);
            }

            foreach (var button in soundButtons)
            {
                button.onClick.AddListener(OnSoundClicked);
            }

            _timer = new Timer(timer, _gameData);
        
            _audioService = new AudioService(music, sound, _configLoader.Load<AudioConfig>(Constants.AudioConfigPath));

            _loop = new Loop(_audioService, _inputService, _timer,
                _configLoader.Load<GameConfig>(Constants.GameConfigPath), _gameData);
        
            _uiService.ShowClear(ScreenID.Menu);

            _loop.OnLevelCompleted += OnResult;
        }

        private void Update()
        {
            _inputService?.OnUpdate();
            _timer?.OnUpdate();
        }

        internal void OnPlayClicked(int levelId)
        {
            _adsService.LoadAd();
            _audioService.PlaySound(0);
            _uiService.ShowClear(ScreenID.Game);
            _loop.StartGame(levelId);
        }

        private void OnSoundClicked()
        {
            _audioService.PlaySound(0);
            _gameData.isSound = _audioService.IsSound();
            foreach (var button in soundButtons)
            {
                button.image.sprite = _gameData.isSound ? sprites[0] : sprites[1];
            }
        }

        internal void OnPauseClicked()
        {
            _timer.Pause();
            _audioService.PlaySound(0);
            _inputService.Pause();
            _uiService.ShowOver(ScreenID.Pause);
        }

        internal void OnHomeClicked()
        {
            _adsService.HideAd();
            _audioService.PlaySound(0);
            _loop.EndGame();
            _uiService.ShowClear(ScreenID.Menu);
        }

        internal void OnGameRestartClicked()
        {
            _adsService.LoadAd();
            _audioService.PlaySound(0);
            _loop.EndGame();
            _loop.StartGame(_gameData.currentLevel);
            _uiService.ShowClear(ScreenID.Game);
        }

        internal void OnResultRestart()
        {
            _adsService.LoadAd();
            _audioService.PlaySound(0);
            _loop.EndGame();
            _loop.StartGame(_gameData.currentLevel - 1);
            _uiService.ShowClear(ScreenID.Game);
        }

        internal void OnLevelsBack()
        {
            _uiService.ShowClear(ScreenID.Menu);
        }

        internal void OnResumeClicked()
        {
            _audioService.PlaySound(0);
            _inputService.Unpause();
            _uiService.HideOver(ScreenID.Pause);
        }

        internal void OnLevelsClicked()
        {
            _audioService.PlaySound(0);
            _uiService.ShowClear(ScreenID.Level);
        }

        private void OnResult()
        {
            _adsService.HideAd();
            _adsService.ShowInterstitialAd();
            _audioService.PlaySound(2);
            _gameData.currentLevel++;
            if (_gameData.currentLevel > 19)
                _gameData.currentLevel = 0;
            _loop.EndGame();
            _uiService.UpdateView(ScreenID.Result);
            _uiService.ShowOver(ScreenID.Result);
        }

        internal void OnSound()
        {
            _audioService.PlaySound(0);
        }
    }
}
