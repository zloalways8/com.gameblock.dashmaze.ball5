using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(menuName = "Configs/Audio")]
    public class AudioConfig : ScriptableObject
    {
        [SerializeField] private AudioClip[] clips;
        public AudioClip[] Clips => clips;
    }
}