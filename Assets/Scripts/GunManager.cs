using UnityEngine;

public class GunManager : MonoBehaviour
{
    public GameObject gunPrefab; // Assign AK-47 model in inspector
    public Transform gunHolster; // Assign Gun Holster object in inspector
    private GameObject currentGun;

    void Start()
    {
        SpawnGun();
    }

    void SpawnGun()
    {
        // Spawn gun as child of holster
        currentGun = Instantiate(gunPrefab, gunHolster.position, gunHolster.rotation);
        currentGun.transform.SetParent(gunHolster);

        // Set local position and rotation to align with holster
        currentGun.transform.localPosition = Vector3.zero;
        currentGun.transform.localRotation = Quaternion.identity;
    }
}