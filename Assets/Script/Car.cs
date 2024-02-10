using System.Collections.Generic;
using Cinemachine;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;
using UnityEngine.SceneManagement;

// Car Collision effects

public class Car : NetworkBehaviour
{
    [SerializeField] private PlayerVisual playerVisual;
    [SerializeField] private List<Vector3> spawnPos;
    [SerializeField] private CinemachineVirtualCamera vc;
    [SerializeField] private AudioListener listener;
    [SerializeField] private NetworkRigidbody rb;
    [SerializeField] private List<Collider> colliderList;
    private PlayerData playerData;
    static public Vector3 POS = Vector3.zero;
    static public Vector3 TRUCKVEL = Vector3.zero;
    public HealthBar healthBar;
    public int ZombDamage = 7;
    public int WallDamageMult = 10;
    //public int TntDamage = 30;
    public int HealthAdd = -50;
    public int HEALTH;
    //public RampBuild Ramp;

    // zomb cooldown
    private bool canTakeZombDamage = true;
    private float zombDamageCooldown = 0.5f;

    // Set Values at start
    void Start()
    {

        playerData = CarGameMultiplayer.Instance.GetPlayerDataFromClientID(OwnerClientId);
        
    }

    public override void OnNetworkSpawn()
    {
        playerVisual.SetBodyColor(CarGameMultiplayer.Instance.GetPlayerColor(playerData.colorId));
        if (SceneManager.GetActiveScene().name == "CharacterSelectScene")
        {
            this.gameObject.transform.GetChild(7).gameObject.SetActive(false);
        }

        if (SceneManager.GetActiveScene().name != "CharacterSelectScene")
        {
            transform.position = spawnPos[CarGameMultiplayer.Instance.GetPlayerDataIndexFromClientID(OwnerClientId)];
        }

        if (IsOwner)
        {
            listener.enabled = true;
            vc.Priority = 1;
        }
        else
        {
            vc.Priority = 0;
        }
    }


    // Check for player death every frame
    void Update()
    {
        if (CheckForDeath(CarGame.Instance.GetHealth()))
        {
            ScoreManager.instance.SaveHighScore();
            LoadMainMenu();
        }
    }

    // Load main menu
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Get the truck velocity and position at a fixed rate
    private void FixedUpdate()
    {
        //UnityEngine.Debug.Log(rb.velocity.magnitude);
        TRUCKVEL = rb.GetComponent<Rigidbody>().velocity;
        Vector3 tPos = transform.position;
        POS = tPos;
    }

    // Collision Detection for car damage
    private void OnTriggerEnter(Collider other)
    {
        colliderList.Add(other);
        // Take car damage from zombie if traveling slower than a certain speed
        if (other.gameObject.CompareTag("Zomb"))
        {
            if (canTakeZombDamage && TRUCKVEL.magnitude <= 4.5)
            {
                CarGame.Instance.TakeDamage(ZombDamage * (1 + KillZomb.killedCount / 10));
                canTakeZombDamage = false;
                Invoke("ResetZombDamageCooldown", zombDamageCooldown);
            }
        }

        // Take car damage when hitting a bomb zombie
        if (other.gameObject.CompareTag("bombZomb"))
        {
            CarGame.Instance.TakeDamage(20);
        }

        // Take car damage when hitting a wall
        if (other.gameObject.CompareTag("Arena"))
        {
            if (TRUCKVEL.magnitude >= 3.5)
            {
                CarGame.Instance.TakeDamage(WallDamageMult);
            }
        }

        // Take car damage when hitting TNT powerup
        if (other.gameObject.CompareTag("TNT"))
        {
            DestroyObjectServerRpc();
            TakeBombDamageClientRpc();
        }

        // Heal car when hitting health boost powerup
        if (other.gameObject.CompareTag("HealthBoost"))
        {
            DestroyObjectServerRpc();
            HealClientRpc();
        }

        colliderList.Clear();
    }

    // Check for player death
    bool CheckForDeath(int health)
    {
        if (health <= 0)
        {
            return true;
        }
        return false;
    }

    void ResetZombDamageCooldown()
    {
        canTakeZombDamage = true;
    }


    [ServerRpc]
    private void DestroyObjectServerRpc()
    {
        Destroy(colliderList[0].gameObject);
    }


    [ClientRpc]
    private void TakeBombDamageClientRpc()
    {
        Destroy(colliderList[0].gameObject);
        CarGame.Instance.TakeDamage(30);
    }

    [ClientRpc]
    private void HealClientRpc()
    {
        Destroy(colliderList[0].gameObject);
        
        Debug.Log(colliderList[0]);

        if ((CarGame.Instance.GetHealth() - -20) > 100)
        {
            CarGame.Instance.TakeDamage(HEALTH - 100);
        }
        else
        {

            CarGame.Instance.TakeDamage(-20);
        }
        //Ramp.canSpawnRamp = true;
    }
}
