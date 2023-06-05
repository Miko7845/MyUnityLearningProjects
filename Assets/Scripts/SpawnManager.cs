using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] animalPrefabs;
    private float spawnRangeX = 20.0f;
    private float spawnRangeZ = 16.0f;
    private float spawnPosZ = 18.0f;
    private float spawnPosX = 22.0f;
    private float startDelay = 2.0f;
    private float spawnInterval = 3f;

    void Start()
    {
        InvokeRepeating("SpawnRandomAnimalOnTop", startDelay, spawnInterval);
        InvokeRepeating("SpawnRandomAnimalOnRight", startDelay, spawnInterval);
        InvokeRepeating("SpawnRandomAnimalOnLeft", startDelay, spawnInterval);
    }

    void SpawnRandomAnimalOnTop()
    {
        int animalIndex = Random.Range(0, animalPrefabs.Length);
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, spawnPosZ);

        Instantiate(animalPrefabs[animalIndex], spawnPos, animalPrefabs[animalIndex].transform.rotation);
    }

    void SpawnRandomAnimalOnRight()
    {
        Vector3 spawnPos = new Vector3(spawnPosX, 0, Random.Range(1, spawnRangeZ));

        int animalIndex = Random.Range(0, animalPrefabs.Length);
        Instantiate(animalPrefabs[animalIndex], spawnPos, Quaternion.Euler(0, -90, 0));
    }

    void SpawnRandomAnimalOnLeft()
    {
        Vector3 spawnPos = new Vector3(-spawnPosX, 0, Random.Range(1, spawnRangeZ));

        int animalIndex = Random.Range(0, animalPrefabs.Length);
        Instantiate(animalPrefabs[animalIndex], spawnPos, Quaternion.Euler(0, 90, 0));
    }
}
