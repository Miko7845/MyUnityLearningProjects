using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public float RotationSpeed;                                                             // Rotation speed of the camera


    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");                                // Get the horizontal input from the keyboard
        transform.Rotate(Vector3.up, horizontalInput * RotationSpeed * Time.deltaTime);     // Rotate the camera around the Y axis
    }
}
