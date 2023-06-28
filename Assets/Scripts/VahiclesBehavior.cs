using UnityEngine;

public class VahiclesBehavior : MonoBehaviour
{
    [SerializeField] float speed = 15.0f;
    
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
