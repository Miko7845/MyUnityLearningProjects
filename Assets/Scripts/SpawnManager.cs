using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] obstaclePrefabs;
    PlayerController playerControllerScript;
    Vector3 spawnPos = new Vector3(25, 0, 0);
    float startDelay = 5;
    float repeatRate = 2;

    void Start()
    {
        InvokeRepeating("CpawnObstacle", startDelay, repeatRate);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void CpawnObstacle()
    {
        if( playerControllerScript.gameOver == false)
        {
            int obstacleIndex = Random.Range(0, obstaclePrefabs.Length);
            Instantiate(obstaclePrefabs[obstacleIndex], spawnPos, obstaclePrefabs[obstacleIndex].transform.rotation);
        }
    }
}