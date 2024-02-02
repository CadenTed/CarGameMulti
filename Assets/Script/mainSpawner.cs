using Unity.Netcode;
using UnityEngine;

// Spawn Manager

public class mainSpawner : NetworkBehaviour
{
    public GameObject zombPrefab;
    public GameObject TntPrefab;
    public GameObject HealthPrefab;
    public GameObject bombZombPrefab;

    private float minSpawnRate = 1f;
    private float maxSpawnRate = 3f;
    private float minBomberSpawnRate = 7f;
    private float maxBomberSpawnRate = 13f;
    private float minPowerSpawn = 15f;
    private float maxPowerSpawn = 30f;

    public static bool canSpawnZomb = false;
    public static bool canSpawnHealth = false;
    public static bool canSpawnBomb = false;
    public static bool canSpawnBombZomb = false;

    // Start spawning of zombies and powerups
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnZomb", Random.Range(minSpawnRate, maxSpawnRate));
        Invoke("SpawnBomb", Random.Range(minPowerSpawn, maxPowerSpawn));
        Invoke("SpawnHealth", Random.Range(minPowerSpawn, maxPowerSpawn));
        Invoke("SpawnBombZomb", Random.Range(minPowerSpawn, maxPowerSpawn));
        /*
        if (Ramp.canSpawnRamp && Input.GetKeyDown("space"))
        {
            Invoke("SpawnRamp", Random.Range(minPowerSpawn, maxPowerSpawn));         
        }
        */
    }

    // check if entites can spawn and spawn accordingly
    private void FixedUpdate()
    {
        if (!IsServer) return;


        if (canSpawnZomb)
        {
            GameObject zomb = Instantiate(zombPrefab, new Vector3(Random.Range(-74, 74), 0.15f, Random.Range(-74, 74)), transform.rotation);
            NetworkObject zombNetworkObject = zomb.GetComponent<NetworkObject>();
            zombNetworkObject.Spawn(true);
            canSpawnZomb = false;
        }
        if (canSpawnBombZomb)
        {
            GameObject bombZomb = Instantiate(bombZombPrefab, new Vector3(Random.Range(-74, 74), 0.15f, Random.Range(-74, 74)), transform.rotation);
            NetworkObject bombZombNetworkObject = bombZomb.GetComponent<NetworkObject>();
            bombZombNetworkObject.Spawn(true);
            canSpawnBombZomb = false;
        }
        if (canSpawnBomb)
        {
            GameObject Tnt = Instantiate(TntPrefab, new Vector3(Random.Range(-24, 24), 2f, Random.Range(-24, 24)), transform.rotation);
            NetworkObject TntNetworkObject = Tnt.GetComponent<NetworkObject>();
            TntNetworkObject.Spawn(true);
            canSpawnHealth = false;
        }
        if (canSpawnHealth)
        {
            GameObject HpBoost = Instantiate(HealthPrefab, new Vector3(Random.Range(-24, 24), 2f, Random.Range(-24, 24)), transform.rotation);
            NetworkObject HpBoostNetworkObject = HpBoost.GetComponent<NetworkObject>();
            HpBoostNetworkObject.Spawn(true);
            canSpawnBomb = false;
        }

    }

    [ClientRpc]
    private void SpawnNewZombieClientRpc()
    {

    }

    [ClientRpc]
    private void SpawnNewPowerUpClientRpc()
    {

    }

    // Recursively spawn zombies
    void SpawnZomb()
    {
        GameObject zomb = Instantiate(zombPrefab, new Vector3(Random.Range(-74, 74), 0.15f, Random.Range(-74, 74)), transform.rotation);
        NetworkObject zombNetworkObject = zomb.GetComponent<NetworkObject>();
        zombNetworkObject.Spawn(true);
        Invoke("SpawnZomb", Random.Range(minSpawnRate, maxSpawnRate));
    }

    // recursively spawn bomb zombies
    void SpawnBombZomb()
    {
        GameObject bombZomb = Instantiate(bombZombPrefab, new Vector3(Random.Range(-74, 74), 0.15f, Random.Range(-74, 74)), transform.rotation);
        NetworkObject bombZombNetworkObject = bombZomb.GetComponent<NetworkObject>();
        bombZombNetworkObject.Spawn(true);
        Invoke("SpawnBombZomb", Random.Range(minBomberSpawnRate, maxBomberSpawnRate));
    }

    // recursively spawn bombs
    void SpawnBomb()
    {
        GameObject Tnt = Instantiate(TntPrefab, new Vector3(Random.Range(-24, 24), 2f, Random.Range(-24, 24)), transform.rotation);
        NetworkObject TntNetworkObject = Tnt.GetComponent<NetworkObject>();
        TntNetworkObject.Spawn(true);
        Invoke("SpawnBomb", Random.Range(minPowerSpawn, maxPowerSpawn));
    }

    // Recursively spawn Health
    void SpawnHealth()
    {
        GameObject HpBoost = Instantiate(HealthPrefab, new Vector3(Random.Range(-24, 24), 2f, Random.Range(-24, 24)), transform.rotation);
        NetworkObject HpBoostNetworkObject = HpBoost.GetComponent<NetworkObject>();
        HpBoostNetworkObject.Spawn(true);
        Invoke("SpawnHealth", Random.Range(minPowerSpawn, maxPowerSpawn));
    }

    /*
    void SpawnRamp()
    {
        if( Ramp.canSpawnRamp && Input.GetKeyDown("space"))
        {
            GameObject ramp = Instantiate(HealthPrefab, new Vector3(Random.Range(-24, 24), 2f, Random.Range(-24, 24)), transform.rotation);
            Invoke("SpawnRamp", Random.Range(minPowerSpawn, maxPowerSpawn));
            Ramp.canSpawnRamp = false;
        }

    }
    */
}
