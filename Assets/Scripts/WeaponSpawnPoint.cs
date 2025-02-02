using UnityEngine;
using Fusion;

public class WeaponSpawnPoint : NetworkBehaviour
{
    [Header("Weapon Prefabs")]
    public NetworkPrefabRef ak47Prefab;
    public NetworkPrefabRef shotgunPrefab;

    [Header("Spawn Settings")]
    public float minSpawnTime = 5f;
    public float maxSpawnTime = 10f;

    [Networked]
    private NetworkObject SpawnedWeapon { get; set; }

    [Networked]
    private float NextSpawnTime { get; set; } = 0;

    [Networked]
    private bool IsOccupied { get; set; } = false;

    public override void FixedUpdateNetwork()
    {
        if (!IsOccupied && Runner.SimulationTime >= NextSpawnTime)
        {
            SpawnRandomWeapon();
        }
    }

     private void SpawnRandomWeapon()
    {
        // Remove InputAuthority, use StateAuthority instead
        NetworkPrefabRef prefabToSpawn = Random.value > 0.5f ? ak47Prefab : shotgunPrefab;

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

    public override void Spawned()
    {
        if (Object.HasStateAuthority)
        {
            StartSpawnTimer();
        }
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
