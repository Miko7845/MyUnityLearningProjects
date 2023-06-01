using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public GameObject dogPrefab;
    bool canSpawn = true;

    // Update is called once per frame
    void Update()
    {
        // On spacebar press, send dog
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (canSpawn)
            {
                Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);
                canSpawn = false;
                Invoke("SetCanSpawn", 0.5f);
            }
        }
    }

    void SetCanSpawn()
    {
        canSpawn = true;
    }
}
