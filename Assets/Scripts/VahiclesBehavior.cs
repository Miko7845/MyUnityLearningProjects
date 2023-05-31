using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VahiclesBehavior : MonoBehaviour
{
    private float speed = 15.0f;

    void Start()
    {
        
    }

    
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
