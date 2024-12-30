using System.Collections.Generic;

namespace Services.UI
{
    public class UiService : IUiService
    {
        private readonly List<Screen> _screens;

        public UiService(List<Screen> screens)
        {
            _screens = screens;
        }
        
        public void ShowClear(ScreenID id)
        {
            for (int i = 0; i < _screens.Count; i++)
            {
                if (_screens[i].id == id)
                {
                    _screens[i].Show();
                }
                else
                {
                    _screens[i].Hide();
                }
            }
        }

        public void ShowOver(ScreenID id)
        {
            for (int i = 0; i < _screens.Count; i++)
            {
                if (_screens[i].id == id)
                {
                    _screens[i].Show();
                }
            }
        }

        public void HideOver(ScreenID id)
        {
            for (int i = 0; i < _screens.Count; i++)
            {
                if (_screens[i].id == id)
                {
                    _screens[i].Hide();
                }
            }
        }

        public void UpdateView(ScreenID id)
        {
            for (int i = 0; i < _screens.Count; i++)
            {
                if (_screens[i].id == id)
                {
                    _screens[i].UpdateView();
                }
            }
        }
    }

    public interface IUiService
    {
        void ShowClear(ScreenID id);
        void ShowOver(ScreenID id);
        void HideOver(ScreenID id);

        void UpdateView(ScreenID id);
    }
}