using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 15.0f;
    [SerializeField] float turnSpeed = 12.0f;
    private float horizontalInput;
    private float verticalInput;

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal"); // -1 to 1
        verticalInput = Input.GetAxis("Vertical"); // -1 to 1

        transform.Translate(Vector3.forward * Time.deltaTime * speed * verticalInput);
        transform.Rotate(Vector3.up * turnSpeed * horizontalInput * Time.deltaTime);
    }
}
