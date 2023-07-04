using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 15.0f;
    [SerializeField] float turnSpeed = 12.0f;

    float horizontalInput;
    float verticalInput;

    void FixedUpdate()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.forward * Time.deltaTime * speed * verticalInput);
        transform.Rotate(Vector3.up * turnSpeed * horizontalInput * Time.deltaTime);
    }
}
