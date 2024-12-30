using System;
using System.Threading.Tasks;
using UnityEngine;

namespace GameCore
{
    public class InputService : IInputService
    {
        public event Action<SwipeDirection> OnSwiped;

        private bool _hasInput;
        
        private float treshHold = 50f;

        private Vector2 start;
        private Vector2 end;
        

        public void OnUpdate()
        {
            if (!_hasInput) return;
            if (Input.GetMouseButtonDown(0))
            {
                start = Input.mousePosition;
            }

            if (Input.GetMouseButtonUp(0))
            {
                end = Input.mousePosition;
                HandleSwipe();
            }
        }
        public void Pause()
        {
            _hasInput = false;
        }

        public async void Unpause()
        {
            await Task.Delay(500);
            _hasInput = true;
        }
        
        public void GameUnpause()
        {
            _hasInput = true;
        }

        private void HandleSwipe()
        {
            float x = end.x - start.x;
            float y = end.y - start.y;

            if (Mathf.Abs(x) > treshHold)
            {
                if (x > 0)
                {
                    OnSwiped?.Invoke(SwipeDirection.Right);
                }
                else
                {
                    OnSwiped?.Invoke(SwipeDirection.Left);
                }
            }
            else if (Mathf.Abs(y) > treshHold)
            {
                if (y > 0)
                {
                    OnSwiped?.Invoke(SwipeDirection.Up);
                }
                else
                {
                    OnSwiped?.Invoke(SwipeDirection.Down);
                }
            }
        }
    }

    public interface IInputService
    {
        event Action<SwipeDirection> OnSwiped; 
        
        void OnUpdate();
        void Pause();
        void Unpause();

        void GameUnpause();


    }

    public enum SwipeDirection
    {
        Up,
        Down,
        Left,
        Right
    }
}