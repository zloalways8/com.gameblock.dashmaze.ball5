using System.Collections.Generic;
using Root;
using UnityEngine.UI;

namespace Services.UI
{
    public class Levels : Screen
    {
        private GameData _gameData;
        public List<LevelButton> levelButtons;
        public Button left;
        public Button right;
        public Button back;

        private int starId = 0;
    

        public override void Init(GameData gameData)
        {
            _gameData = gameData;
        
        }

        public override void BindButtons(EntryPoint entryPoint)
        {
            left.onClick.AddListener(OnLeftClicked);
            right.onClick.AddListener(OnRightClicked);
        
            left.onClick.AddListener(entryPoint.OnSound);
            right.onClick.AddListener(entryPoint.OnSound);
            back.onClick.AddListener(entryPoint.OnLevelsBack);

            for (int i = 0; i < levelButtons.Count; i++)
            {
                var index = i;
                levelButtons[i].button.onClick.AddListener(() =>
                {
                    entryPoint.OnPlayClicked(levelButtons[index].id);
                });
            }
        }

        private void OnLeftClicked()
        {
            starId = 0;
            UpdateView();
            left.interactable = false;
            right.interactable = true;
        }

        private void OnRightClicked()
        {
            starId = 9;
            UpdateView();
            right.interactable = false;
            left.interactable = true;
        }

        public override void Show()
        {
            starId = 0;
            UpdateView();
            left.interactable = false;
            base.Show();
        }

        public override void UpdateView()
        {
            for (int i = 0; i < levelButtons.Count; i++)
            {
                levelButtons[i].id = starId + i;
                levelButtons[i].levelId.text = (levelButtons[i].id + 1).ToString();
                if (levelButtons[i].id < _gameData.currentLevel)
                {
                    levelButtons[i].toggle.gameObject.SetActive(true);
                    levelButtons[i].text.gameObject.SetActive(true);
                    levelButtons[i].button.interactable = true;
                    levelButtons[i].levelId.gameObject.SetActive(true);
                    levelButtons[i].latch.gameObject.SetActive(false);
                
                }
                else if (levelButtons[i].id == _gameData.currentLevel)
                {
                    levelButtons[i].toggle.gameObject.SetActive(false);
                    levelButtons[i].text.gameObject.SetActive(false);
                    levelButtons[i].button.interactable = true;
                    levelButtons[i].levelId.gameObject.SetActive(true);
                    levelButtons[i].latch.gameObject.SetActive(false);
                }
                else
                {
                    levelButtons[i].toggle.gameObject.SetActive(false);
                    levelButtons[i].text.gameObject.SetActive(false);
                    levelButtons[i].button.interactable = false;
                    levelButtons[i].levelId.gameObject.SetActive(false);
                    levelButtons[i].latch.gameObject.SetActive(true);
                }
            }
        }
    }
}