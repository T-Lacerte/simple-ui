using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SimpleUI.Core
{
    public class SceneManager : MonoBehaviour
    {
        // [SerializeField, Tooltip("The fade script of the loading screen background.")]
        // private ImageFade loadingScreenFade;
        private GameObject loadingAnimationObject;
        private bool isLoadingScene;
        private bool isLoadingGroup;
        private bool isUnloadingScene;
        private bool isUnloadingGroup;
        public string LastSceneName { get; private set; }
        public string CurrentSceneName { get; private set; }
        public bool IsLoading => isLoadingScene || isLoadingGroup;
        public bool IsUnloading => isUnloadingScene || isUnloadingGroup;
        public event SceneLoadStartEvent OnLoadStart;
        public event SceneLoadEndEvent OnLoadEnd;
        public event SceneUnloadStartEvent OnUnloadStart;
        public event SceneUnloadEndEvent OnUnloadEnd;
        public event SceneGroupLoadStartEvent OnGroupLoadStart;
        public event SceneGroupLoadEndEvent OnGroupLoadEnd;
        public event SceneGroupUnloadStartEvent OnGroupUnloadStart;
        public event SceneGroupUnloadEndEvent OnGroupUnloadEnd;

        private void Awake()
        {
            //loadingAnimationObject = loadingScreenFade.transform.Find(GameObjects.LOADING).gameObject;
        }

        public Coroutine Load(string sceneName)
        {
            SaveLastScene();
            return StartCoroutine(LoadRoutine(sceneName));
        }
        public Coroutine Unload(string sceneName)
        {
            return StartCoroutine(UnloadRoutine(sceneName));
        }
        
        public Coroutine LoadGroup(SceneGroup sceneGroup, bool showLoading = false)
        {
            SaveLastScene();
            return StartCoroutine(LoadGroupRoutine(sceneGroup, showLoading));
        }
        public Coroutine UnloadGroup(SceneGroup sceneGroup)
        {
            return StartCoroutine(UnloadGroupRoutine(sceneGroup));
        }
        

        public Coroutine LoadLastScene()
        {
            return Load(LastSceneName);
        }

        private void SaveLastScene()
        {
            LastSceneName = CurrentSceneName;
        }

        private IEnumerator LoadRoutine(string sceneName)
        {
            NotifyLoadStart(sceneName);
            
            var sceneToLoad = UnityEngine.SceneManagement.SceneManager.GetSceneByName(sceneName);
            if (!sceneToLoad.isLoaded)
                yield return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            CurrentSceneName = sceneName;
            NotifyLoadEnd(sceneName);
        }
        private IEnumerator UnloadRoutine(string sceneName)
        {
            NotifyUnloadStart(sceneName);
            var sceneToUnload = UnityEngine.SceneManagement.SceneManager.GetSceneByName(sceneName);
            if (sceneToUnload.isLoaded)
                yield return UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(sceneName);
            NotifyUnloadEnd(sceneName);
        }
        
        private IEnumerator LoadGroupRoutine(SceneGroup sceneGroup, bool showLoading)
        {
            NotifyGroupLoadStart(sceneGroup);
            // Afficher l'écran de loading
            // if (showLoading)
            // {
            //     yield return loadingScreenFade.FadeIn();
            //     loadingAnimationObject.SetActive(true);
            // }
            
            foreach (var scene in sceneGroup.Scenes)
            {
                var sceneToLoad = UnityEngine.SceneManagement.SceneManager.GetSceneByName(scene.Name);
                if (!sceneToLoad.isLoaded)
                    yield return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(scene.Name, LoadSceneMode.Additive);
            }

            if (sceneGroup.SetFirstAsActive && sceneGroup.Scenes.Any())
            {
                var sceneToMakeActive = UnityEngine.SceneManagement.SceneManager.GetSceneByName(sceneGroup.Scenes[0].Name);

                if (sceneToMakeActive.isLoaded)
                {
                    UnityEngine.SceneManagement.SceneManager.SetActiveScene(sceneToMakeActive);
                    CurrentSceneName = sceneGroup.Scenes[0];
                }
            }
            
            NotifyGroupLoadEnd(sceneGroup);

            // if (showLoading)
            // {
            //     loadingAnimationObject.SetActive(false);
            //     yield return loadingScreenFade.FadeOut();
            // }
                
        }
        private IEnumerator UnloadGroupRoutine(SceneGroup sceneBundle)
        {
            NotifyGroupUnloadStart(sceneBundle);

            foreach (var scene in sceneBundle.Scenes)
            {
                var sceneToUnload = UnityEngine.SceneManagement.SceneManager.GetSceneByName(scene.Name);
                if (sceneToUnload.isLoaded)
                    yield return UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(scene.Name);
            }

            NotifyGroupUnloadEnd(sceneBundle);
        }

        private void NotifyGroupLoadStart(SceneGroup sceneGroup)
        {
            isLoadingGroup = true;

            OnGroupLoadStart?.Invoke(sceneGroup);
        }

        private void NotifyGroupLoadEnd(SceneGroup sceneGroup)
        {
            isLoadingGroup = false;

            OnGroupLoadEnd?.Invoke(sceneGroup);
        }
        
        private void NotifyGroupUnloadStart(SceneGroup sceneGroup)
        {
            isUnloadingGroup = true;

            OnGroupUnloadStart?.Invoke(sceneGroup);
        }

        private void NotifyGroupUnloadEnd(SceneGroup sceneGroup)
        {
            isUnloadingGroup = false;

            OnGroupUnloadEnd?.Invoke(sceneGroup);
        }
        private void NotifyLoadStart(string sceneName)
        {
            isLoadingScene = true;

            OnLoadStart?.Invoke(sceneName);
        }

        private void NotifyLoadEnd(string sceneName)
        {
            isLoadingScene = false;

            OnLoadEnd?.Invoke(sceneName);
        }
        
        private void NotifyUnloadStart(string sceneName)
        {
            isUnloadingScene = true;

            OnUnloadStart?.Invoke(sceneName);
        }

        private void NotifyUnloadEnd(string sceneName)
        {
            isUnloadingScene = false;

            OnUnloadEnd?.Invoke(sceneName);
        }
    }
    public delegate void SceneLoadStartEvent(string sceneName);
    public delegate void SceneLoadEndEvent(string sceneName);
    public delegate void SceneUnloadStartEvent(string sceneName);
    public delegate void SceneUnloadEndEvent(string sceneName);
    public delegate void SceneGroupLoadStartEvent(SceneGroup sceneGroup);
    public delegate void SceneGroupLoadEndEvent(SceneGroup sceneGroup);
    public delegate void SceneGroupUnloadStartEvent(SceneGroup sceneGroup);
    public delegate void SceneGroupUnloadEndEvent(SceneGroup sceneGroup);
}