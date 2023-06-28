using UnityEngine;

public class SpinPropellerX : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 1000.0f;

    void Update()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}
