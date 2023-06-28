using UnityEngine;

public class FollowPlayerX : MonoBehaviour
{
    [SerializeField] GameObject plane;
    [SerializeField] Vector3 offset = new Vector3(30, 0, 10);

    void LateUpdate()
    {
        transform.position = plane.transform.position + offset;
    }
}
