using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

[InfoHeaderClass("Drag me into the scene. I can load new scenes")]
public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance;   

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);         
    }

    public void LoadNextScene()
    {
        if (UpgradeManager.Instance != null)
        { Destroy(UpgradeManager.Instance.gameObject); }
        if (SoundManager.Instance != null)
        {Destroy(SoundManager.Instance.gameObject);}
        int nextSceneBuildInt = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneBuildInt);
    }

    public void LoadSpecificScene(string sceneName)
    {
        if (UpgradeManager.Instance != null)
        { Destroy(UpgradeManager.Instance.gameObject); }
        if (SoundManager.Instance != null)
        {Destroy(SoundManager.Instance.gameObject);}
        SceneManager.LoadScene(sceneName);
    }

    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //Only works for executables.
    public void QuitGame()
    {
        Application.Quit();
    }
}
