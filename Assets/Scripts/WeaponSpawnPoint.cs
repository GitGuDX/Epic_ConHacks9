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
        NetworkPrefabRef prefabToSpawn = Random.value > 0.5f ? ak47Prefab : shotgunPrefab;

        var weaponObject = Runner.Spawn(prefabToSpawn, transform.position, transform.rotation, Object.InputAuthority, (runner, obj) =>
        {
            obj.transform.SetParent(transform);
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
