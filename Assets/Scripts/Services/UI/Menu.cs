using Root;
using TMPro;
using UnityEngine.UI;

namespace Services.UI
{
    public class Menu : Screen
    {
        public Button play;
        public Button levels;
        public TextMeshProUGUI currentLevel;
        
        private GameData _gameData;

        public override void Init(GameData gameData)
        {
            _gameData = gameData;
        }

        public override void BindButtons(EntryPoint entryPoint)
        {
            play.onClick.AddListener(() =>
            {
                entryPoint.OnPlayClicked(_gameData.currentLevel);
            });
            levels.onClick.AddListener(entryPoint.OnLevelsClicked);
        }

        public override void UpdateView()
        {
            currentLevel.text = (_gameData.currentLevel + 1).ToString();
        }

        public override void Show()
        {
            UpdateView();
            base.Show();
        }

        public override void Hide()
        {
            base.Hide();
        }
    }
}