using UnityEngine;
public class PlanetSpawner : MonoBehaviour
{
    [SerializeField] GameObject planetPrefab;
    [SerializeField] public  float spawnRate = 2f;
    [SerializeField] public float spawnXPosition = 12f;
    [SerializeField] public float minSpacing = 2f; 
    [SerializeField] private int lasSpawnedIndex = -1;
    [SerializeField] private int spawnCount = 0;

    private Vector2[] spawnRanges = new Vector2[]
    {
        new Vector2(5f, 10f), //high
        new Vector2(0f, 5f), // mid
        new Vector2(-5f, 0f), // low
    };
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
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
        while (newSpawnIndex == lasSpawnedIndex && spawnCount >= 2); 
        
        if (newSpawnIndex  != lasSpawnedIndex) 
        {
            spawnCount = 0;
        }
        spawnCount++;
        lasSpawnedIndex = newSpawnIndex;
        
        float randomY  = Random.Range(spawnRanges[newSpawnIndex].x, spawnRanges[newSpawnIndex].y);
        Vector2 spawnPosition = new Vector2(spawnXPosition, randomY);

        if (!Physics2D.OverlapCircle(spawnPosition, minSpacing))
        {
            GameObject newPlanet =Instantiate(planetPrefab, spawnPosition, Quaternion.identity);
            newPlanet.transform.SetParent(transform);
        } else 
        {
            Debug.Log("Planet too close to the previous one");
        }
    }

}
