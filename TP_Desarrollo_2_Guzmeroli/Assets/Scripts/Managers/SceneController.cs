using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : Singleton<SceneController>
{
    [SerializeField] List<SceneAsset> sceneList;

    protected override void Initialize()
    {
        base.Initialize();

        DontDestroyOnLoad(gameObject);
    }

    public void LoadSceneAsyncSingle(int index)
    {
        StartCoroutine(LoadAsynchronously(index, LoadSceneMode.Single));
    }

    public void LoadSceneAsyncAdditive(int index)
    {
        StartCoroutine(LoadAsynchronously(index, LoadSceneMode.Additive));
    }

    public void UnloadScene(int index)
    {
        StartCoroutine(UnloadAsync(index));
    }

    public void UnloadScene(string name)
    {
        StartCoroutine(UnloadAsync(name));
    }

    IEnumerator LoadAsynchronously(int sceneIndex, LoadSceneMode mode)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex, mode);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            yield return null;
        }
    }

    IEnumerator UnloadAsync(int index)
    {
        AsyncOperation operation = SceneManager.UnloadSceneAsync(index);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            yield return null;
        }
    }

    IEnumerator UnloadAsync(string name)
    {
        AsyncOperation operation = SceneManager.UnloadSceneAsync(name);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            yield return null;
        }
    }
}
