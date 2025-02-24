using UnityEngine;

public class PlanetSpawner : MonoBehaviour
{
    public GameObject planetPrefab;
    public Transform playerTransform;
    public float spawnRate = 2f;
    public float spawnDistanceAhead = 15f;
    public float minSpacing = 2f;

    private int lastSpawnIndex = -1;
    private int spawnCounter = 0;

    private Vector2[] spawnRanges = new Vector2[]
    {
        new Vector2(5f, 10f),
        new Vector2(-5f, 5f),
        new Vector2(-10f, -5f)
    };

    private void Start()
    {
        InvokeRepeating(nameof(SpawnPlanet), 1f, spawnRate);
    }

    private void SpawnPlanet()
    {
        int newSpawnIndex;
        do
        {
            newSpawnIndex = Random.Range(0, spawnRanges.Length);
        }
        while (newSpawnIndex == lastSpawnIndex && spawnCounter >= 2);

        if (newSpawnIndex != lastSpawnIndex)
        {
            spawnCounter = 0;
        }
        spawnCounter++;
        lastSpawnIndex = newSpawnIndex;

        float randomY = Random.Range(spawnRanges[newSpawnIndex].x, spawnRanges[newSpawnIndex].y);
        float spawnXPosition = playerTransform.position.x + spawnDistanceAhead;
        Vector2 spawnPosition = new Vector2(spawnXPosition, randomY);

        if (!Physics2D.OverlapCircle(spawnPosition, minSpacing))
        {
            GameObject newPlanet = Instantiate(planetPrefab, spawnPosition, Quaternion.identity);
            newPlanet.transform.SetParent(transform);
        }
    }
}