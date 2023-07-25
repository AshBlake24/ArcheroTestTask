using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Source.Infrastructure
{
    public class SceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner) => 
            _coroutineRunner = coroutineRunner;

        public void Load(string name, Action onLoaded = null) => 
            _coroutineRunner.StartCoroutine(LoadScene(name, onLoaded));

        private IEnumerator LoadScene(string name, Action onLoaded)
        {
            if (SceneManager.GetActiveScene().name == name)
            {
                onLoaded?.Invoke();
                yield break;
            }
            
            AsyncOperation nextScene = SceneManager.LoadSceneAsync(name);

            while (nextScene.isDone == false)
                yield return null;
            
            onLoaded?.Invoke();
        }
    }
}