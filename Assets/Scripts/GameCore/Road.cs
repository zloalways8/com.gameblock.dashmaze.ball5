using UnityEngine;

namespace GameCore
{
    public class Road : MonoBehaviour
    {
        public new Renderer renderer;
        public bool isMarked;

        private Color colorToMark;
        
        public int x;
        public int y;

        public void Init(Color color)
        {
            colorToMark = color;
            isMarked = false;
        }

        public void Mark()
        {
            renderer.material.color = colorToMark;
            isMarked = true;
        }
    }
}