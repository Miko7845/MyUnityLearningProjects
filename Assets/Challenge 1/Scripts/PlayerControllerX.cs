using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    [SerializeField] float speed = 12.0f;
    [SerializeField] float rotationSpeed = 25.0f;
    private float verticalInput;

    void FixedUpdate()
    {
        verticalInput = Input.GetAxis("Vertical");                                          // get the user's vertical input
        transform.Translate(Vector3.forward * speed * Time.deltaTime);                      // move the plane forward at a constant rate
        transform.Rotate(Vector3.left * rotationSpeed * verticalInput * Time.deltaTime);    // tilt the plane up/down based on up/down arrow keys
    }
}
