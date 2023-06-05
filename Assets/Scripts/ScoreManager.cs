using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int score = 0;
    public int lives { get; private set; } = 3;

    public void IncrementScore()
    {
        score++;
        Debug.Log("Score: " + score);
    }

    public void DecrementLives()
    {
        lives--;
        Debug.Log("Lives: " + lives);
    }
}
