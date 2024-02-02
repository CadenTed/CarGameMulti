using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

// Pause Menu Manager

public class PauseManager : NetworkBehaviour
{
    public static PauseManager Instance { get; private set; }

    public event EventHandler OnMultiplayerGamePaused;
    public event EventHandler OnMultiplayerGameUnpaused;




    public GameObject pauseMenuUI;
    private bool isLocalPaused = false;
    private NetworkVariable<bool> isPaused = new NetworkVariable<bool>(false);
    private Dictionary<ulong, bool> playerPausedDictionary;

    // Make sure menu doesn't popup
    void Awake()
    {
        Instance = this;

        playerPausedDictionary = new Dictionary<ulong, bool>();
        pauseMenuUI.SetActive(false);
    }

    // Check if escape key is pressed and pause game if it is
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isLocalPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public override void OnNetworkSpawn()
    {
        isPaused.OnValueChanged += IsGamePaused_OnValueChanged;   
    }

    private void IsGamePaused_OnValueChanged(bool previousValue, bool newValue)
    {
        if (isPaused.Value)
        {
            Time.timeScale = 0f;

            OnMultiplayerGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f;

            OnMultiplayerGameUnpaused?.Invoke(this, EventArgs.Empty);
        }
    }

    // Resume Game
    public void Resume()
    {
        UnPauseGameServerRpc();

        Debug.Log("RESUME");
        pauseMenuUI.SetActive(false);
        isLocalPaused = false;
    }

    // pause game

    void Pause()
    {
        PauseGameServerRpc();

        Debug.Log("PAUSED");
        pauseMenuUI.SetActive(true);
        isLocalPaused = true;
    }

    // Load main menu
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    [ServerRpc(RequireOwnership = false)]
    private void PauseGameServerRpc(ServerRpcParams serverRpcParams = default)
    {
        playerPausedDictionary[serverRpcParams.Receive.SenderClientId] = true;
        TestGamePauseState();
    }

    [ServerRpc(RequireOwnership = false)]
    private void UnPauseGameServerRpc(ServerRpcParams serverRpcParams = default)
    {
        playerPausedDictionary[serverRpcParams.Receive.SenderClientId] = false;
        TestGamePauseState();
    }

    private void TestGamePauseState()
    {
        foreach (ulong clientId in NetworkManager.Singleton.ConnectedClientsIds)
        {
            if (playerPausedDictionary.ContainsKey(clientId) && playerPausedDictionary[clientId])
            {
                // This player is paused
                isPaused.Value = true;
                return;
            }
        }

        // All players unpaused
        isPaused.Value = false;
    }
}