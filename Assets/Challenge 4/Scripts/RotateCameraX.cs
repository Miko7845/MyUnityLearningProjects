using UnityEngine;

public class RotateCameraX : MonoBehaviour
{
    [SerializeField] GameObject player;
    float speed = 200;

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, horizontalInput * speed * Time.deltaTime);
        // Move focal point with player
        transform.position = player.transform.position; 
    }
}