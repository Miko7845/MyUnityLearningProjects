using UnityEngine;

[RequireComponent(typeof(TrailRenderer), typeof(BoxCollider))]
public class Swipe : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    Camera cam;
    Vector3 mousePosition;
    BoxCollider col;
    TrailRenderer trail;
    bool swiping = false;

    void Awake()
    {
        cam = Camera.main;
        col = GetComponent<BoxCollider>();
        trail = GetComponent<TrailRenderer>();
        col.enabled = false;
        trail.enabled = false;
    }

    void Update()
    {
        if (gameManager.isGameActive && trail != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                swiping = true;
                UpdateComponents();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                swiping = false;
                UpdateComponents();
            }

            if(swiping)
            {
                UpdateMousePosition();
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Target>())
        {
            collision.gameObject.GetComponent<Target>().DestroyTarget();
        }
    }

    void UpdateMousePosition()
    {
        mousePosition = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
        transform.position = mousePosition;
    }

    public void UpdateComponents()
    {
        col.enabled = swiping;
        trail.enabled = swiping;
    }
}