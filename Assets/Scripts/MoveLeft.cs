using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    PlayerController playerControllerScript;
    float speed = 25;
    float dashSpeed = 45;
    float leftBound = -15;
    

    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if (playerControllerScript.gameOver == false && !Input.GetKey(KeyCode.LeftControl) && playerControllerScript.positionToMoveTo.x <= playerControllerScript.transform.position.x)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
            playerControllerScript.score += 1 * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.LeftControl) && !playerControllerScript.gameOver && playerControllerScript.positionToMoveTo.x <= playerControllerScript.transform.position.x)
        {
            transform.Translate(Vector3.left * Time.deltaTime * dashSpeed);
            playerControllerScript.score += 3 * Time.deltaTime;
        }
        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}