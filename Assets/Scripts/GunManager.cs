using UnityEngine;

public class GunManager : MonoBehaviour
{
    public GameObject gunPrefab;
    public Transform gunHolster;
    private GameObject currentGun;

    void Start()
    {
       // if (gunPrefab != null)
        //    SpawnGun(gunPrefab);
    }

    // OnTriggerEnter is called when the Player collider enters the trigger
    void OnTriggerEnter(Collider other)
    {
        Pickupable pickupable = other.GetComponent<Pickupable>();
        if (pickupable != null)
        {
            // Destroy current gun if exists
            if (currentGun != null)
                Destroy(currentGun);

            // Spawn new gun
            SpawnGun(pickupable.gunPrefab);

            // Destroy pickup
            Destroy(other.gameObject);

            // Free spawn point
            ItemSpawner spawner = FindFirstObjectByType<ItemSpawner>();
            if (spawner != null)
                spawner.FreeSpawnPoint(other.transform.parent);
        }
    }

    void SpawnGun(GameObject gunToSpawn)
    {
        currentGun = Instantiate(gunToSpawn, gunHolster.position, gunHolster.rotation);
        currentGun.transform.SetParent(gunHolster);
        currentGun.transform.localPosition = Vector3.zero;
        currentGun.transform.localRotation = Quaternion.identity;
    }
}