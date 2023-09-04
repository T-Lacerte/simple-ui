using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SimpleUI.Core
{
    [CreateAssetMenu(fileName = "New Scene Group", menuName = "Game/Scene Group")]
    public class SceneGroup : ScriptableObject
    {
        [SerializeField] private List<SceneRef> scenes = new();
        [SerializeField] private bool setFirstAsActive;
        
        public IReadOnlyList<SceneRef> Scenes => scenes;
        
        public bool SetFirstAsActive => setFirstAsActive;
        
        public bool IsLoaded => scenes.All(it => UnityEngine.SceneManagement.SceneManager.GetSceneByName(it.Name).isLoaded);
    }
}