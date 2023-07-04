using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    [SerializeField] float speed = 12.0f;
    [SerializeField] float rotationSpeed = 25.0f;
    float verticalInput;

    void FixedUpdate()
    {
        verticalInput = Input.GetAxis("Vertical");                                      
        transform.Translate(Vector3.forward * speed * Time.deltaTime);                  
        transform.Rotate(Vector3.left * rotationSpeed * verticalInput * Time.deltaTime);
    }
}
