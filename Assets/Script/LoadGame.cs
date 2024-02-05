using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Scene Loading Manager
// Reset kill count on scene change

public class LoadGame : NetworkBehaviour
{
    [SerializeField] private Button mainGameBtn;

    private void Awake()
    {
        mainGameBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("LobbyScene");
        });
    }

    public void LoadTutorial()
    {
        SceneManager.LoadScene("Demo");
        Time.timeScale = 0f;
        KillZomb.killedCount = 0;
    }

    public void LoadMain()
    {
        SceneManager.LoadScene("MainGame");
        KillZomb.killedCount = 0;
    }

    public void QuitGame()
    {
        Application.Quit();
        KillZomb.killedCount = 0;
    }

    public void StartTutorial()
    {
        Time.timeScale = 1f;
        KillZomb.killedCount = 0;
    }

    public static void LoadCharacterSelect()
    {
        NetworkManager.Singleton.SceneManager.LoadScene("CharacterSelectScene", LoadSceneMode.Single);
    }

    public static void LoadLobbyScene()
    {
        NetworkManager.Singleton.SceneManager.LoadScene("LobbyScene", LoadSceneMode.Single);
    }

    public static void LoadMultiplayerGame()
    {
        NetworkManager.Singleton.SceneManager.LoadScene("MainGame", LoadSceneMode.Single); 
    }

    public static void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
