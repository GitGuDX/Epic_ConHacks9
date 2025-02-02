using UnityEngine;
using System.Collections.Generic;

public class ItemSpawner : MonoBehaviour
{
    [System.Serializable]
    public class SpawnableItem
    {
        public GameObject itemPrefab;
        public float spawnWeight = 1f;
    }

    public SpawnableItem[] spawnableItems;
    public Transform[] spawnPoints;
    public float minSpawnTime = 10f;
    public float maxSpawnTime = 30f;
    
    private List<Transform> availableSpawnPoints;
    private float nextSpawnTime;

    void Start()
    {
        availableSpawnPoints = new List<Transform>(spawnPoints);
        SetNextSpawnTime();
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime && availableSpawnPoints.Count > 0)
        {
            SpawnRandomItem();
            SetNextSpawnTime();
        }
    }

    void SpawnRandomItem()
    {
        if (availableSpawnPoints.Count == 0) return;

        // Select random spawn point
        int spawnIndex = Random.Range(0, availableSpawnPoints.Count);
        Transform spawnPoint = availableSpawnPoints[spawnIndex];
        
        // Select random item based on weights
        float totalWeight = 0;
        foreach (var item in spawnableItems)
            totalWeight += item.spawnWeight;

        float randomWeight = Random.Range(0, totalWeight);
        SpawnableItem selectedItem = spawnableItems[0];
        
        float currentWeight = 0;
        foreach (var item in spawnableItems)
        {
            currentWeight += item.spawnWeight;
            if (randomWeight <= currentWeight)
            {
                selectedItem = item;
                break;
            }
        }

        // Spawn the item
        GameObject spawnedItem = Instantiate(selectedItem.itemPrefab, 
            spawnPoint.position, 
            spawnPoint.rotation);

        // Remove used spawn point
        availableSpawnPoints.RemoveAt(spawnIndex);
    }

    void SetNextSpawnTime()
    {
        nextSpawnTime = Time.time + Random.Range(minSpawnTime, maxSpawnTime);
    }

    // Call this when an item is picked up to free up the spawn point
    public void FreeSpawnPoint(Transform spawnPoint)
    {
        if (!availableSpawnPoints.Contains(spawnPoint))
        {
            availableSpawnPoints.Add(spawnPoint);
        }
    }
}
