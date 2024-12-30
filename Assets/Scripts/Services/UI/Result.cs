using Root;
using TMPro;
using UnityEngine.UI;

namespace Services.UI
{
    public class Result : Screen
    {
        public TextMeshProUGUI timer;
        public Button home;
        public Button restart;
        public Button next;
        
        private GameData _gameData;

        public override void Init(GameData gameData)
        {
            _gameData = gameData;
        }
        
        public override void BindButtons(EntryPoint entryPoint)
        {
            home.onClick.AddListener(entryPoint.OnHomeClicked);
            restart.onClick.AddListener(entryPoint.OnResultRestart);
            next.onClick.AddListener(() =>
            {
                entryPoint.OnPlayClicked(_gameData.currentLevel);
            });
        }

        public override void UpdateView()
        {
            timer.text = _gameData.time;
        }
    }
}