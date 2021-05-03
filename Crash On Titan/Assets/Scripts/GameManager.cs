using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public int score;
    public int retry;
    public int retryCount;
    public bool retryed;

    public Transform respawnPoint;
    public Transform player;

    public Text scoreText;
    public Text livesText;

    public PlayerController pc;

    public GameObject upgradeMenu;
    public GameObject gameOverUI;
    public GameObject useBtn;
    public GameObject retryBtn;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        retry = PlayerPrefs.GetInt("Retry");
        if(retry != 1)
        {
            retryed = false;
        }
        else
        {
            retryed = true;
        }
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        gameOverUI.SetActive(false);
        upgradeMenu.SetActive(false);
        UpdateLives();
        UpdateScore();

    }

    private void OnLevelWasLoaded(int level)
    {
        Time.timeScale = 1f;
        score = PlayerPrefs.GetInt("Score");
        livesText.text = pc.lives.ToString(); 
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScore();
    }

    public void AddScore(int value)
    {
        score += value;
        scoreText.text = "Score: " + score.ToString();
    }

    public void Respawn()
    {
        player.position = respawnPoint.position;
    }
    
    public void UpdateScore()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public void UpdateLives()
    {
        livesText.text = "Lives: " + pc.lives.ToString();
    }


    public void GameOver()
    {
        gameOverUI.SetActive(true);
        if (retryed)
        {
            retryBtn.SetActive(false);
        }
        else;
        Time.timeScale = 0f;

    }

    public void Retry()
    {
        PlayerPrefs.SetInt("Score", score);
        pc.lives++;
        retryCount++;
        PlayerPrefs.SetInt("Lives", pc.lives);
        UpdateLives();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.SetInt("Retry", retryCount);
    }

    public void UpgradeMenu()
    {
        upgradeMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Continue()
    {
        upgradeMenu.SetActive(false);
        Time.timeScale = 1f;
    }
}
