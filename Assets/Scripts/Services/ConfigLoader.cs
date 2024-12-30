using UnityEngine;

namespace Services
{
    public class ConfigLoader : IConfigLoader
    {
        public T Load<T>(string path) where T : ScriptableObject
        {
            return Resources.Load<T>(path);
        }
    }

    public interface IConfigLoader
    {
        T Load<T>(string path) where T : ScriptableObject;
    }
}