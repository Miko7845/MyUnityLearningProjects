using UnityEngine;

public class Bullet : MonoBehaviour
{
    Transform target;               // Field to store a reference to the target
    float speed = 15.0f;                                                                    
    bool homing;                    // Field to store the state of the bullet (whether it is chasing the target or not)
    float bulletStrength = 15.0f;
    float aliveTimer = 5.0f;        // Field to store the lifetime of the bullet

    void Update()
    {
        // If the bullet is chasing the target and the target is not null
        if (homing && target != null)
        {
            Vector3 moveDirection = (target.transform.position - transform.position).normalized;
            transform.position += moveDirection * speed * Time.deltaTime;
            transform.LookAt(target);                                                               // Rotate the bullet so that it looks at the target
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (target != null)
        {
            // If the object that collided with the bullet has the same tag as the target
            if (col.gameObject.CompareTag(target.tag))
            {
                Rigidbody targetRigidbody = col.gameObject.GetComponent<Rigidbody>();
                Vector3 away = -col.contacts[0].normal;
                targetRigidbody.AddForce(away * bulletStrength, ForceMode.Impulse);
                Destroy(gameObject);
            }
        }
    }

    public void Fire(Transform newTarget)
    {
        target = newTarget;                 // Assign the field target a reference to the new target
        homing = true;                      // Set the field homing to true, so that the bullet starts chasing the target
        Destroy(gameObject, aliveTimer);
    }
}