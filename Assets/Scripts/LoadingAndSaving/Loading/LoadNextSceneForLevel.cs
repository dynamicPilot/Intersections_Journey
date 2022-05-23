using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextSceneForLevel : LoadNextScene
{
    [SerializeField] private GameMaster gameMaster;
    public void LoadAdditiveSceneAsync(string newSceneName)
    {
        SceneManager.LoadSceneAsync(newSceneName, LoadSceneMode.Additive).completed += (asyncHandler) =>
        {
            loadingPanel.gameObject.SetActive(false);
            gameMaster.SetNewLevel();
        };
    }

    public void ContinueLoading()
    {
        loadingPanel.ContinueLoading();
        loadingPanel.gameObject.SetActive(true);
    }
}
