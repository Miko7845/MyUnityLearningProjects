using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;                                                            // Rigidbody component of the player
    private GameObject focalPoint;                                                         // Focal point of the camera
    public float speed = 5.0f;                                                             // Speed of the player

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();                                              // Get the Rigidbody component of the player
        focalPoint = GameObject.Find("Focal Point");                                       // Get the focal point of the camera
    }

    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");                                    // Get the vertical input from the keyboard
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);            // Move the player forward/backward, relative to the focal point.
    }
}
