using System.Collections;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    [SerializeField] GameObject powerupIndicator;
    [SerializeField] ParticleSystem smokeParticle;
    [SerializeField] bool hasPowerup;
    [SerializeField] int powerUpDuration = 7;
    Rigidbody playerRb;
    GameObject focalPoint;
    float speed = 500;
    float boostSpeed = 1500;
    float normalStrength = 10;      // how hard to hit enemy without powerup
    float powerupStrength = 25;     // how hard to hit enemy with powerup
    
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    void Update()
    {
        // Add force to player in direction of the focal point (and camera)
        float verticalInput = Input.GetAxis("Vertical");
        if (Input.GetKey(KeyCode.Space))
        {
            playerRb.AddForce(focalPoint.transform.forward * verticalInput * boostSpeed * Time.deltaTime);
            smokeParticle.Play();
            smokeParticle.transform.position = transform.position + new Vector3(0, -0.4f, 0);
        }
        else if (!Input.GetKey(KeyCode.Space))
        {
            playerRb.AddForce(focalPoint.transform.forward * verticalInput * speed * Time.deltaTime);
            smokeParticle.Stop();
        }
        // Set powerup indicator position to beneath player
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.6f, 0);        
    }

    // If Player collides with powerup, activate powerup
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            hasPowerup = true;
            StartCoroutine(PowerupCooldown());
            powerupIndicator.SetActive(true);
        }
    }

    // If Player collides with enemy
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer =  other.gameObject.transform.position - transform.position;
            // if have powerup hit enemy with powerup force, if no powerup, hit enemy with normal strength 
            if (hasPowerup) 
            {
                enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            }
            else
            {
                enemyRigidbody.AddForce(awayFromPlayer * normalStrength, ForceMode.Impulse);
            }
        }
    }

    // Coroutine to count down powerup duration
    IEnumerator PowerupCooldown()
    {
        yield return new WaitForSeconds(powerUpDuration);
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }
}