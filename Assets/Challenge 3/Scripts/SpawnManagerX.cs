using UnityEngine;

public class SpawnManagerX : MonoBehaviour
{
    [SerializeField] GameObject[] objectPrefabs;
    PlayerControllerX playerControllerScript;
    float spawnDelay = 2;
    float spawnInterval = 1.5f;

    void Start()
    {
        InvokeRepeating("SpawnObjects", spawnDelay, spawnInterval);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerControllerX>();
    }

    void SpawnObjects ()
    {
        Vector3 spawnLocation = new Vector3(30, Random.Range(5, 15), 0);
        int index = Random.Range(0, objectPrefabs.Length);
        if (playerControllerScript.gameOver == false)
        {
            Instantiate(objectPrefabs[index], spawnLocation, objectPrefabs[index].transform.rotation);
        }
    }
}