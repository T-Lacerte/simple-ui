using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SimpleUI.Core
{
    public class SceneLoader : MonoBehaviour
    {

        public Coroutine Load(string sceneName, Action preLoadAction = null, Action postLoadAction = null)
        {
            return StartCoroutine(LoadRoutine(sceneName, preLoadAction, postLoadAction));
        }
        public Coroutine Unload(string sceneName, Action preUnloadAction = null, Action postUnloadAction = null)
        {
            return StartCoroutine(UnloadRoutine(sceneName, preUnloadAction, postUnloadAction));
        }

        private IEnumerator LoadRoutine(string sceneName, Action preLoadAction, Action postLoadAction)
        {
            preLoadAction?.Invoke();
            var sceneToLoad = SceneManager.GetSceneByName(sceneName);
            if (!sceneToLoad.isLoaded)
                yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            postLoadAction?.Invoke();
        }
        private IEnumerator UnloadRoutine(string sceneName, Action preUnloadAction, Action postUnloadAction)
        {
            preUnloadAction?.Invoke();
            var sceneToUnload = SceneManager.GetSceneByName(sceneName);
            if (sceneToUnload.isLoaded)
                yield return SceneManager.UnloadSceneAsync(sceneName);
            postUnloadAction?.Invoke();
        }
        

     
    }
}