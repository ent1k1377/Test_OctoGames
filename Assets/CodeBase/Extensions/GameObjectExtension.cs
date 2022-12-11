using UnityEngine;

namespace CodeBase.Extensions
{
    public static class GameObjectExtension
    {
        public static void Activate(this GameObject go) =>
            go.SetActive(true);
        
        public static void Deactivate(this GameObject go) =>
            go.SetActive(false);
    }
}