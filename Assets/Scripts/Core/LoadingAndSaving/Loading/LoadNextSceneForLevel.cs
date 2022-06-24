using UnityEngine.SceneManagement;

public class LoadNextSceneForLevel : LoadNextScene
{
    public void LoadAdditiveSceneAsync(string newSceneName, LevelGameManager gameMaster)
    {
        SceneManager.LoadSceneAsync(newSceneName, LoadSceneMode.Additive).completed += (asyncHandler) =>
        {
            loadingPanel.gameObject.SetActive(false);
            gameMaster.LevelIsReady();
        };
    }

    public void ContinueLoading()
    {
        loadingPanel.ContinueLoading();
        loadingPanel.gameObject.SetActive(true);
    }
}
