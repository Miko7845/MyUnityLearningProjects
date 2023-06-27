using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI livesText;
    public Button restartButton;
    public Slider volumeSlider;
    public GameObject titleScreen;
    public GameObject pauseScreen;
    internal bool isGameActive;
    public AudioSource sound;

    private int score;
    private float spawnRate = 1.0f;
    private int lives = 3;

    private void Start()
    {
        sound.volume = volumeSlider.value;
    }

    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            PauseGame();
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void PlayerDied()
    {
        if(lives == 0)
        {
            GameOver();
        }
        else
        {
            lives -= 1;
            livesText.text = "Lives: " + lives;
        }
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        isGameActive = true;
        score = 0;
        lives = 3;
        spawnRate /= difficulty;

        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        livesText.text = "Lives: " + lives;

        titleScreen.gameObject.SetActive(false);
    }

    public void AdjustMusicVolume()
    {
        sound.volume = volumeSlider.value;
    }

    public void PauseGame()
    {
        if(isGameActive && Time.timeScale == 1)
        {
            Time.timeScale = 0;
            sound.Pause();
            pauseScreen.gameObject.SetActive(true);
            isGameActive = false;
        }
        else if (!isGameActive && Time.timeScale == 0)
        {
            Time.timeScale = 1;
            sound.Play();
            pauseScreen.gameObject.SetActive(false);
            isGameActive = true;
        }
    }
}
