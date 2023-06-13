using System.Collections;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    private Rigidbody playerRb;
    private GameObject focalPoint;
    public GameObject powerupIndicator;
    public ParticleSystem smokeParticle;

    private float speed = 500;
    private float boostSpeed = 1500;
    public bool hasPowerup;
    public int powerUpDuration = 7;
    private float normalStrength = 10;                                                              // how hard to hit enemy without powerup
    private float powerupStrength = 25;                                                             // how hard to hit enemy with powerup
    
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");                                            // Add force to player in direction of the focal point (and camera)

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

        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.6f, 0);        // Set powerup indicator position to beneath player

    }

    // If Player collides with powerup, activate powerup
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            hasPowerup = true;
            StartCoroutine(PowerupCooldown());
            powerupIndicator.SetActive(true);
        }
    }

    // Coroutine to count down powerup duration
    IEnumerator PowerupCooldown()
    {
        yield return new WaitForSeconds(powerUpDuration);
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }

    // If Player collides with enemy
    private void OnCollisionEnter(Collision other)
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



}
