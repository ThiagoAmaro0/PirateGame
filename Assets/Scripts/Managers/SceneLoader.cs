using System;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private void LoadScene(string sceneName, LoadSceneMode mode, Action callback)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName, mode);
        op.completed += (ctx) =>
        {
            callback?.Invoke();
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
        };
    }
    public void LoadScene(string sceneName, LoadSceneMode mode, bool unloadCurrent)
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if (SceneManager.GetSceneAt(i).name == sceneName)
            {
                return;
            }
        }
        if (unloadCurrent)
        {
            UnloadActiveScene(() => LoadScene(sceneName, mode, null));
        }
        else
        {
            LoadScene(sceneName, mode, null);
        }
    }
    public void LoadScene(string sceneName, LoadSceneMode mode, bool unloadCurrent, Action callback)
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if (SceneManager.GetSceneAt(i).name == sceneName)
            {
                return;
            }
        }
        if (unloadCurrent)
        {
            UnloadActiveScene(() => LoadScene(sceneName, mode, callback));
        }
        else
        {
            LoadScene(sceneName, mode, callback);
        }
    }

    public bool UnloadScene(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if (SceneManager.GetSceneAt(i).name == sceneName)
            {
                SceneManager.UnloadSceneAsync(sceneName);
                return true;
            }
        }
        return false;
    }

    public void UnloadActiveScene(Action callback)
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene()).completed += (ctx) => callback?.Invoke();
    }
}