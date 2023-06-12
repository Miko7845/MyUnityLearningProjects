using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;                                                            // Rigidbody component of the player
    private GameObject focalPoint;                                                         // Focal point of the camera
    public GameObject powerupIndicator;
    public float speed = 5.0f;                                                             // Speed of the player
    public bool hasPowerup = false;
    private float powerupStrength = 15.0f;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();                                              // Get the Rigidbody component of the player
        focalPoint = GameObject.Find("Focal Point");                                       // Get the focal point of the camera
    }

    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");                                    // Get the vertical input from the keyboard
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);            // Move the player forward/backward, relative to the focal point.
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
            powerupIndicator.SetActive(true);
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
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;

            Debug.Log("Collided with " + collision.gameObject.name + " with powerup set to " + hasPowerup);
            enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
        }
    }
}
