using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject[] powerupPrefabs;
    private float spawnRange = 9f;
    public int enemyCount;
    public int waveNumber = 1;

    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;                                                 // The number of remaining enemies. 

        // If enemies are defeated, spawn a new wave and powerup.
        if (enemyCount == 0)
        {
            SpawnWave(waveNumber);
        }
    }

    void SpawnWave(int enemiesToSpawn)
    {
        int powerupIndex = Random.Range(0, powerupPrefabs.Length);
        Instantiate(powerupPrefabs[powerupIndex], GenerateSpawnPosition(), powerupPrefabs[powerupIndex].transform.rotation);

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int enemyIndex = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[enemyIndex], GenerateSpawnPosition(), enemyPrefabs[enemyIndex].transform.rotation);     
        }

        waveNumber++;
    }

    // Return a Vector3 with the random X and Y positions.
    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosY = Random.Range(-spawnRange, spawnRange);
        return new Vector3(spawnPosX, 0.15f, spawnPosY);
    }
}
