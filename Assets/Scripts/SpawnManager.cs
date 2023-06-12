using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;
    private float spawnRange = 9f;
    public int enemyCount;
    public int waveNumber = 1;

    void Start()
    {
        SpawnEnemyWave(waveNumber);                                                                     // Spawn enemy
        Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);          // Spawn powerup
    }

    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;                                                 // The number of remaining enemies. 

        // If enemies are defeated, spawn a new wave and powerup.
        if (enemyCount == 0)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
            Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
        }
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);     
        }
    }

    // Return a Vector3 with the random X and Y positions.
    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosY = Random.Range(-spawnRange, spawnRange);
        return new Vector3(spawnPosX, 0.15f, spawnPosY);
    }
}
