using UnityEngine;

public class GunManager : MonoBehaviour
{
    public GameObject gunPrefab;
    public Transform gunHolster;
    private GameObject currentGun;
    private Shoot shootScript;

    void Start()
    {
        shootScript = GetComponent<Shoot>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other == null) return;

        Pickupable pickupable = other.GetComponent<Pickupable>();
        if (pickupable != null)
        {
            Transform spawnPoint = other.transform.parent;

            // Store reference before destroying
            ItemSpawner spawner = FindFirstObjectByType<ItemSpawner>();

            // Update gun and shooting component
            if (currentGun != null)
                Destroy(currentGun);

            SpawnGun(pickupable.gunPrefab);
            if (shootScript != null)
            {
                shootScript.weaponType = pickupable.gunType;
                shootScript.SetHasGun(true);
            }

            // Free spawn point before destroying pickup
            if (spawner != null && spawnPoint != null)
            {
                spawner.FreeSpawnPoint(spawnPoint);
            }

            // Destroy pickup last
            Destroy(other.gameObject);
        }
    }

    public void SpawnGun(GameObject gunToSpawn)
    {
        currentGun = Instantiate(gunToSpawn, gunHolster.position, gunHolster.rotation);
        currentGun.transform.SetParent(gunHolster);
        currentGun.GetComponent<Collider>().enabled = false;
        currentGun.transform.localPosition = Vector3.zero;
        currentGun.transform.localRotation = Quaternion.identity;
    }
}