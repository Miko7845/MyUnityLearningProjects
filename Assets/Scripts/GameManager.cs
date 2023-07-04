using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public bool isGameActive;

    [SerializeField] List<GameObject> targets;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI gameOverText;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] Button restartButton;
    [SerializeField] Slider volumeSlider;
    [SerializeField] GameObject titleScreen;
    [SerializeField] GameObject pauseScreen;
    [SerializeField] AudioSource sound;
    int score;
    float spawnRate = 1.0f;
    int lives = 3;

    void Start()
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