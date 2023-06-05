using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
    ScoreManager scoreManager;

    private void Start()
    {
        scoreManager = GameObject.Find("GameScoreManager").GetComponent<ScoreManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
            scoreManager.IncrementScore();
        }
    }
}
