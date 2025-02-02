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
    private float NextSpawnTime { get; set; }
    
    [Networked]
    private bool IsOccupied { get; set; }

    public override void Spawned()
    {
        if (Object.HasStateAuthority)
        {
            StartSpawnTimer();
        }
    }

    public override void FixedUpdateNetwork()
    {
        if (!Object.HasStateAuthority) return;

        if (!IsOccupied && Runner.SimulationTime >= NextSpawnTime)
        {
            SpawnRandomWeapon();
        }
    }

    private void SpawnRandomWeapon()
    {
        if (!Object.HasStateAuthority) return;

        NetworkPrefabRef prefabToSpawn = Random.value > 0.5f ? ak47Prefab : shotgunPrefab;

        var weaponObject = Runner.Spawn(prefabToSpawn, transform.position, transform.rotation, Object.InputAuthority, (runner, obj) =>
        {
            obj.transform.SetParent(transform);
        });

        SpawnedWeapon = weaponObject;
        IsOccupied = true;
    }

    private void StartSpawnTimer()
    {
        if (!Object.HasStateAuthority) return;
        NextSpawnTime = Runner.SimulationTime + Random.Range(minSpawnTime, maxSpawnTime);
    }
}