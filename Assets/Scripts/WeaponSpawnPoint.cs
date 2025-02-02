using UnityEngine;
using Fusion;

public class WeaponSpawnPoint : NetworkBehaviour
{
    [Header("Weapon Prefabs")]
    public GameObject ak47Prefab;
    public GameObject shotgunPrefab;
    public GameObject pistolPrefab;

    [Header("Spawn Settings")]
    public float minSpawnTime = 5f;
    public float maxSpawnTime = 10f;

    [Networked]
    private NetworkObject SpawnedWeapon { get; set; }

    [Networked]
    private float NextSpawnTime { get; set; } = 0;

    [Networked]
    private bool IsOccupied { get; set; } = false;

    public override void Spawned() {
        if (Object.HasStateAuthority)
        {
            StartSpawnTimer();
        }
    }

    void Update() {
        try {
            SpawnedWeapon.transform.position = transform.position;
        } catch {}
    }

    public override void FixedUpdateNetwork()
    {
        if (!IsOccupied && Runner.SimulationTime >= NextSpawnTime)
        {
            SpawnRandomWeapon();
        }
    }

     private void SpawnRandomWeapon()
    {
        // Choose a random value between 0 and 1
        float randomValue = Random.value;
        GameObject prefabToSpawn;

        // Randomly choose a weapon to spawn
        if (randomValue < 0.33f)
            prefabToSpawn = ak47Prefab;
        else if (randomValue < 0.66f)
            prefabToSpawn = shotgunPrefab;
        else
            prefabToSpawn = pistolPrefab;

        var weaponObject = Runner.Spawn(
            prefabToSpawn,
            transform.position,
            transform.rotation,
            Runner.LocalPlayer, // Use LocalPlayer instead of InputAuthority
            (runner, obj) =>
            {
                // Set position before parenting
                obj.transform.position = transform.position;
                obj.transform.rotation = transform.rotation;

                // Use Fusion's object parenting
                var networkObject = obj.GetComponent<NetworkObject>();
                if (networkObject != null)
                {
                    networkObject.transform.SetParent(transform);
                }
            });

        SpawnedWeapon = weaponObject;
        IsOccupied = true;
    }

    public void OnWeaponPickedUp()
    {
        IsOccupied = false;
        StartSpawnTimer();
    }

    private void StartSpawnTimer()
    {
        NextSpawnTime = Runner.SimulationTime + Random.Range(minSpawnTime, maxSpawnTime);
    }
}
