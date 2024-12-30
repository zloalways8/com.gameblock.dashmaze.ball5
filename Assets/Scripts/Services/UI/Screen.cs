using Root;
using UnityEngine;

namespace Services.UI
{
    public enum ScreenID
    {
        Menu,
        Game,
        Level,
        Result,
        Pause
    }
    
    public abstract class Screen : MonoBehaviour
    {
        public ScreenID id;
        public abstract void Init(GameData gameData);

        public virtual void BindButtons(EntryPoint entryPoint){}
        public virtual void UnBindButtons(EntryPoint entryPoint){}


        public virtual void Show()
        {
            this.gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            this.gameObject.SetActive(false);
        }

        public virtual void UpdateView()
        {
            
        }
    }
}