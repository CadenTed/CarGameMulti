using System;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Search;
using UnityEngine.UI;

// Check for tutorial ending condition

public class CarGame : NetworkBehaviour

{
    public static CarGame Instance { get; private set; }

    public TextMeshProUGUI scoreText;

    public Scene currentScene;

    [SerializeField] private HealthBar healthBar;

    [SerializeField] private Transform playerPrefab;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int health;

    private void Awake()
    {
        Instance = this;

        currentScene = SceneManager.GetActiveScene();
    }

    private void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public int GetHealth()
    {
        return health;
    }

    public void SetHealth(int newHealth)
    {
        health = newHealth;
    }
    
    // Update is called once per frame
    void Update()
    {
        // Debug.Log(scoreText.text);
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
    
    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(health);
        healthBar.SetHealth(health);
    }
}
