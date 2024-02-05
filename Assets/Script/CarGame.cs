using System;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

// Check for tutorial ending condition

public class CarGame : NetworkBehaviour

{
    public static CarGame Instance { get; private set; }

    public TextMeshProUGUI scoreText;

    public Scene currentScene;

    [SerializeField] private Transform playerPrefab;

    private void Awake()
    {
        Instance = this;

        currentScene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(scoreText.text);
        if (KillZomb.killedCount >= 10 && currentScene.name == "Demo")
        {
            SceneManager.LoadScene("MainMenu");
            KillZomb.killedCount = 0;
        }
    }

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            NetworkManager.Singleton.SceneManager.OnLoadEventCompleted += SceneManager_OnLoadEventCompleted;
        }

    }

    private void SceneManager_OnLoadEventCompleted(string sceneName, LoadSceneMode loadSceneMode, System.Collections.Generic.List<ulong> clientsCompleted, System.Collections.Generic.List<ulong> clientsTimedOut)
    {
        foreach (ulong clientId in NetworkManager.Singleton.ConnectedClientsIds)
        {
            Transform playerTransform = Instantiate(playerPrefab);
            playerTransform.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId, true);
        }
    }
}
