using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Services.UI
{
    public class LevelButton : MonoBehaviour
    {
        public Button button;
        public Image text;
        public Image toggle;

        public TextMeshProUGUI levelId;
        public int id;

        public Image latch;
    }
}