using System;
using UnityEngine;

namespace PuzzleGame
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static readonly Lazy<T> LazyInstance = new Lazy<T>(CreateInstance);
        public static T Instance => LazyInstance.Value;
        
        static T CreateInstance()
        {
            var ob = FindObjectOfType<T>();
            if (ob != null)
            {
                ob.name = typeof(T).Name + " (singleton)";
                DontDestroyOnLoad(ob);
                return ob;  
            }

            // If not found T object
            var ownerObject = new GameObject($"{typeof(T).Name} (singleton)");
            var instance = ownerObject.AddComponent<T>();
            DontDestroyOnLoad(ownerObject);

            return instance; 
        }
    }
    
}