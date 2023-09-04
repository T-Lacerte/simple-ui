using System;
using UnityEditor;
using UnityEngine;
using Object = System.Object;

namespace SimpleUI.Core
{
    [Serializable]
    public class SceneRef
    {
        [SerializeField] private Object asset;
        [SerializeField] private string name = "";

#if UNITY_EDITOR
        public SceneAsset Asset => asset as SceneAsset;
#endif
        public string Name => name;

        public static implicit operator string(SceneRef sceneRefField)
        {
            return sceneRefField.Name;
        }

        protected bool Equals(SceneRef other)
        {
            return string.Equals(name, other.name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((SceneRef) obj);
        }

        public override int GetHashCode()
        {
            return name != null ? name.GetHashCode() : 0;
        }
    }
}