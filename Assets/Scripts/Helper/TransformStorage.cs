using System.Collections.Generic;
using UnityEngine;

namespace Helper
{
    public static class TransformStorage
    {
        public static Dictionary<string, Transform> Transforms = new Dictionary<string, Transform>();

        public static void RegisterTransform(string name, Transform transform)
        {
            if (!Transforms.ContainsKey(name))
            {
                Transforms.Add(name, transform);
            }
        }
        public static void UnregisterTransform(string name)
        {
            if (Transforms.ContainsKey(name))
            {
                Transforms.Remove(name);
            }
        }

        public static Transform GetTarnsform(string name)
        {
            if (Transforms.ContainsKey(name))
            {
                return Transforms[name];
            }
            else
            {
                Debug.LogError("Name of Transform dont contains");

                return null;
            }
        }
        public static void Reset()
        {
            Transforms.Clear();
        }
    }
}
