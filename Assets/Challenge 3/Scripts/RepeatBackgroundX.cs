using UnityEngine;

public class RepeatBackgroundX : MonoBehaviour
{
    Vector3 startPos;
    float repeatWidth;

    void Start()
    {
        startPos = transform.position;                        
        repeatWidth = GetComponent<BoxCollider>().size.x / 2; 
    }

    void Update()
    {
        if (transform.position.x < startPos.x - repeatWidth)
        {
            transform.position = startPos;
        }
    }
}