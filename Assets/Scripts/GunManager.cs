using UnityEngine;
using Fusion;

public class GunManager : NetworkBehaviour
{
    public GameObject gunPrefab;
    public Transform gunHolster;
    private GameObject currentGun;
    private Shoot shootScript;
    private GunData currentGunData;
    private AmmoSystem ammoSystem;

    void Awake()
    {
        shootScript = GetComponent<Shoot>();
        ammoSystem = GetComponent<AmmoSystem>();  // AmmoSystem on player
    }

    void OnTriggerEnter(Collider other)
    {
        if (other == null) return;

        Pickupable pickupable = other.GetComponent<Pickupable>();
        if (pickupable == null)
        {
            Debug.LogWarning("No Pickupable component found on: " + other.name);
            return;
        }
         if (pickupable?.gunData == null)
        {
            Debug.LogError("No GunData assigned to Pickupable on: " + other.name);
            return;
        }

        Transform spawnPoint = other.transform.parent;

        // Store reference before destroying
        WeaponSpawnPoint spawner = spawnPoint?.GetComponent<WeaponSpawnPoint>();

        // Update gun and shooting component
        if (currentGun != null)
        {
            Destroy(currentGun);
        }

        // Spawn new gun and update data
        PickupGun(pickupable.gunData.gunPrefab);

        if (shootScript != null)
        {
            currentGunData = pickupable.gunData;
            shootScript.SetGunData(currentGunData);
            shootScript.SetHasGun(true);
            ammoSystem.SetGunData(currentGunData);
        }

        // Notify spawn point of pickup
        spawner?.OnWeaponPickedUp();

        // Destroy pickup last
        Runner.Despawn(other.GetComponent<NetworkObject>());
    }

    //Spawn gun at holster position when picked up
    public void PickupGun(GameObject gunToSpawn)
    {
        currentGun = Instantiate(gunToSpawn, gunHolster.position, gunHolster.rotation);
        currentGun.transform.SetParent(gunHolster);
        currentGun.GetComponent<Collider>().enabled = false;
        currentGun.transform.localPosition = Vector3.zero;
        currentGun.transform.localRotation = Quaternion.identity;
    }
}
