using Root;
using UnityEngine.UI;

namespace Services.UI
{
    public class Pause : Screen
    {
        public Button home;
        public Button sound;
        public Button restart;
        public Button resume;
        
        private GameData _gameData;

        public override void Init(GameData gameData)
        {
            _gameData = gameData;
        }
        
        public override void BindButtons(EntryPoint entryPoint)
        {
            home.onClick.AddListener(entryPoint.OnHomeClicked);
            restart.onClick.AddListener(entryPoint.OnGameRestartClicked);
            resume.onClick.AddListener(entryPoint.OnResumeClicked);
        }
    }
}