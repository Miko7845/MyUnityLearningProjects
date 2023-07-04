using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PowerUpType currentPowerUp = PowerUpType.None;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject powerupIndicator;                                                         
    [SerializeField] float speed = 5.0f;      // Player's speed.
    [SerializeField] bool hasPowerup = false;     // False - default setting.
    [SerializeField] float hangTime;
    [SerializeField] float smashSpeed;
    [SerializeField] float explosionForce;
    [SerializeField] float explosionRadius;
    GameObject tmpRocket;
    Coroutine powerupCountdown;
    Rigidbody playerRb;
    GameObject focalPoint;      // Focal point of the camera
    float floorY;
    float powerupStrength = 15.0f;
    bool smashing = false;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce((focalPoint.transform.forward).normalized * speed * forwardInput);    // Move the player forward/backward, relative to the focal point.
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);    // The indicator follows the player
        if (currentPowerUp == PowerUpType.Bulllets && Input.GetKeyDown(KeyCode.LeftControl))
        {
            BulletShoot();
        }
        if (currentPowerUp == PowerUpType.Smash && Input.GetKeyDown(KeyCode.Space) && !smashing)
        {
            smashing = true;
            StartCoroutine(Smash());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            currentPowerUp = other.gameObject.GetComponent<PowerUp>().powerUpType;
            Destroy(other.gameObject);  // Destruction of powerup.
            StartCoroutine(PowerupCountdownRoutine());  // Wait for N seconds
            powerupIndicator.SetActive(true);   // Activate indicator visibility.
            if (powerupCountdown != null)
            {
                StopCoroutine(powerupCountdown);
            }
            powerupCountdown = StartCoroutine(PowerupCountdownRoutine());
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // In the event of a collision with a strengthened player, the enemy flies away.
        if (collision.gameObject.CompareTag("Enemy") && currentPowerUp == PowerUpType.Pushback)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;
            enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
        }
    }

    void BulletShoot()
    {
        foreach (var enemy in FindObjectsOfType<Enemy>())
        {
            tmpRocket = Instantiate(bulletPrefab, transform.position + Vector3.up,
            Quaternion.identity);
            tmpRocket.GetComponent<Bullet>().Fire(enemy.transform);
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        currentPowerUp = PowerUpType.None;
        powerupIndicator.SetActive(false);
    }

    IEnumerator Smash()
    {
        var enemies = FindObjectsOfType<Enemy>();
        //Store the y position before taking off
        floorY = transform.position.y;
        //Calculate the amount of time we will go up
        float jumpTime = Time.time + hangTime;
        while (Time.time < jumpTime)
        {
            //move the player up while still keeping their x velocity.
            playerRb.velocity = new Vector2(playerRb.velocity.x, smashSpeed);
            yield return null;
        }
        //Now move the player down
        while (transform.position.y > floorY)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, -smashSpeed * 2);
            yield return null;
        }
        //Cycle through all enemies.
        for (int i = 0; i < enemies.Length; i++)
        {
            //Apply an explosion force that originates from our position.
            if (enemies[i] != null)
                enemies[i].GetComponent<Rigidbody>().AddExplosionForce(explosionForce,
                transform.position, explosionRadius, 0.0f, ForceMode.Impulse);
        }
        smashing = false;   //We are no longer smashing, so set the boolean to false
    }
}