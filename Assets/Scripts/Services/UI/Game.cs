using Root;
using UnityEngine.UI;

namespace Services.UI
{
    public class Game : Screen
    {
        public Button pause;

        private GameData _gameData;

        public override void Init(GameData gameData)
        {
            _gameData = gameData;
        }
        
        public override void BindButtons(EntryPoint entryPoint)
        {
            pause.onClick.AddListener(entryPoint.OnPauseClicked);
        }
    }
}