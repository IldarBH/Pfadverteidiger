using UnityEngine;

public class MainMenuScene : MonoBehaviour
{
    private StartupCanvas _startupCanvas;

    void Awake()
    {
        _startupCanvas = GameObject.Find("StartupCanvas").GetComponent<StartupCanvas>();        
        _startupCanvas.gameObject.SetActive(true);
        _startupCanvas.Initialize(this);
    }

    public void GoToNewCareer()
    {
        if (!GameManager.StartNewCareer())
        {
            Debug.LogError("Failed to start a new career.");
            return;
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene("Career", UnityEngine.SceneManagement.LoadSceneMode.Single);
    }

    public void GoToContinueCareer()
    {
        if (!GameManager.LoadCareer())
        {
            Debug.LogWarning("No saved career found.");
            return;
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene("Career", UnityEngine.SceneManagement.LoadSceneMode.Single);
    }

    public void GoToSettings()
    {
        // TODO: Implement settings menu navigation
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
