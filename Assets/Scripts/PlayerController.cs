using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private GameObject focalPoint;                                                              // Focal point of the camera
    public GameObject powerupIndicator;                                                         
    public float speed = 5.0f;                                                                  // Player's speed.
    public bool hasPowerup = false;                                                             // False - default setting.
    private float powerupStrength = 15.0f;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);                 // Move the player forward/backward, relative to the focal point.
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);    // The indicator follows the player
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);                                                          // Destruction of powerup.
            StartCoroutine(PowerupCountdownRoutine());                                          // Wait for N seconds
            powerupIndicator.SetActive(true);                                                   // Activate indicator visibility.
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // In the event of a collision with a strengthened player, the enemy flies away.
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;

            enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);         
        }
    }
}
