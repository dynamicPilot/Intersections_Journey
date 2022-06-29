using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using IJ.Utilities.Configs;

//[DefaultExecutionOrder(-1)]
public class LoadNextScene : MonoBehaviour
{ 
    public enum MODE { single, additive }

    [SerializeField] protected LoadingPanel loadingPanel;
    [SerializeField] private GameConfig config;

    public void LoadSceneByName(string newSceneName, MODE mode = MODE.single)
    {
        StartCoroutine(LoadAsynchronously(newSceneName, mode));
    }

    IEnumerator LoadAsynchronously(string sceneName, MODE mode = MODE.single)
    {
        AsyncOperation operation;

        if (mode == MODE.single)
        {
            operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        }
        else
        {
            operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }

        if (loadingPanel != null)
        {
            loadingPanel.StartLoading();
            loadingPanel.gameObject.SetActive(true);

            while (operation.isDone == false)
            {
                loadingPanel.UpdateProgress(Mathf.Clamp01(operation.progress / 0.9f));
                yield return null;
            }
        }
    }

    public void LoadMainManu()
    {
        LoadSceneByName(config.MenuSceneName);
    }

    //public void ReloadCurrentLevel()
    //{
    //    Scene currentScene = SceneManager.GetActiveScene();
    //    LoadSceneByName(currentScene.name);
    //}

    //public void LoadLevelWithScenes(string firstSceneName, string secondSceneName)
    //{
    //    // load first scene --> single
    //    StartCoroutine(LoadTwoScenesAsync(firstSceneName, secondSceneName));
    //}

    //IEnumerator LoadTwoScenesAsync(string firstSceneName, string secondSceneName)
    //{
    //    AsyncOperation operation = SceneManager.LoadSceneAsync(firstSceneName, LoadSceneMode.Single);

    //    while (!operation.isDone)
    //    {
    //        yield return null;
    //    }

    //    operation = SceneManager.LoadSceneAsync(secondSceneName, LoadSceneMode.Additive);
        
    //    while (!operation.isDone)
    //    {
    //        yield return null;
    //    }
    //    //for (int i = 0; i <sceneNames.Length; i++)
    //    //{
    //    //    if (i == 0)
    //    //        operation = SceneManager.LoadSceneAsync(sceneNames[i], LoadSceneMode.Single);
    //    //    else
    //    //        operation = SceneManager.LoadSceneAsync(sceneNames[i], LoadSceneMode.Additive);

    //    //    while (operation.isDone == false)
    //    //    {
    //    //        //float progress = Mathf.Clamp01(operation.progress / 0.9f);
    //    //        //loadingSlider.value = progress;
    //    //        yield return null;
    //    //    }
    //    //}


    //}
}
