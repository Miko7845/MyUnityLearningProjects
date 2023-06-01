using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 16.0f;
    private float xRange = 17.5f;
    private float zRange = 2.0f;
    private float horizontalInput;
    private float verticalInput;

    private ScoreManager scoreManager;
    public GameObject projectilePrefab;
    private bool canSpawn = true;

    void Start()
    {
        // Find the ScoreManager script and assign it to the scoreManager variable
        scoreManager = GameObject.Find("GameScoreManager").GetComponent<ScoreManager>();
    }

    void Update()
    {
        // Keep the player in bounds
        if(transform.position.x < -xRange)
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        else if (transform.position.x > xRange)
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        else if (transform.position.z > zRange + 3)
            transform.position = new Vector3(transform.position.x, transform.position.y, zRange + 3);
        else if (transform.position.z < -zRange)
            transform.position = new Vector3(transform.position.x, transform.position.y, -zRange);

        // Move the player based on arrow key input
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
        transform.Translate(Vector3.forward * verticalInput * Time.deltaTime * speed);

        // Detect when the spacebar is pressed down and instantiate a projectile prefab at the player's position
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
            canSpawn = false;
            Invoke("SetCanSpawn", 0.4f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the player collides with an enemy, destroy the enemy and decrement the player's lives by 1.
        if(other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);

            if(scoreManager.lives > 0)
                scoreManager.DecrementLives();
            else
            {
                Destroy(gameObject);
                Debug.Log("Game Over!");
            }
                
        }
    }

    // Костыль
    private void SetCanSpawn() => canSpawn = true;
}
