using UnityEngine;
using UnityEngine.SceneManagement;

public enum ESceneType
{
    Title,
    Lobby,
    InGame
}
public class SceneLoader : MonoBehaviour
{
    public void LoadScene(ESceneType sceneType)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneType.ToString());
    }

    public void ReloadScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public AsyncOperation LoadSceneAsync(ESceneType sceneType)
    {
        Time.timeScale = 1f;
        return SceneManager.LoadSceneAsync(sceneType.ToString());
    }
}
