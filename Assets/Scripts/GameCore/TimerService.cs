using Services;
using TMPro;
using UnityEngine;

namespace GameCore
{
    public class Timer : ITimer
    {
        private TextMeshProUGUI _timer;
        private bool isOn;
        private float time;
        private GameData _gameData;


        public Timer(TextMeshProUGUI timer, GameData gameData)
        {
            _gameData = gameData;
            _timer = timer;
            isOn = false;
        }
        
        public void OnUpdate()
        {
            if (isOn)
            {
                time += Time.deltaTime;
                UpdateView();
            }
        }

        public void Reset()
        {
            _gameData.time = _timer.text;
            time = 0;
            UpdateView(); 
            isOn = false;
        }

        public void Pause()
        {
            isOn = false;
        }

        public void Unpause()
        {
            isOn = true;
        }

        private void UpdateView()
        {
            int minutes = Mathf.FloorToInt(time / 60);
            int seconds = Mathf.FloorToInt(time % 60);
            _timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    public interface ITimer
    {
        void OnUpdate();
        void Reset();
        void Pause();
        void Unpause();
    }
}