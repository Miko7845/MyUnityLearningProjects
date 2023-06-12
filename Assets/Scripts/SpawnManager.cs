using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;                                                             // GameObject of the enemy
    private float spawnRange = 9f;                                                             // Spawn range for the enemy

    void Start()
    {
        Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);     //  Instantiate(spawn) an enemy.
    }

    void Update()
    {
        
    }

    // Return a Vector3 with the random X and Y positions.
    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosY = Random.Range(-spawnRange, spawnRange);
        return new Vector3(spawnPosX, 0.15f, spawnPosY);
    }
}
