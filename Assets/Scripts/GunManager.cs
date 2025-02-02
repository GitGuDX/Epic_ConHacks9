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
        Pickupable pickupable = other.GetComponent<Pickupable>();
        if (pickupable != null)
        {
            if (currentGun != null)
                Destroy(currentGun);

            SpawnGun(pickupable.gunPrefab);
            // Update weapon type in Shoot script
            if (shootScript != null)
                shootScript.weaponType = pickupable.gunType;

            Destroy(other.gameObject);

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