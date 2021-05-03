using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private int currentSceneIndex;

    public static bool m_gameIsPause = false;
    public GameObject PauseMenuUI;
    public GameObject SettingsMenuUI;

    public GameManager gm;
    public PlayerController pc;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        PauseMenuUI.SetActive(false);
        SettingsMenuUI.SetActive(false);
    }

    // Update is called once per frame
    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        m_gameIsPause = false;
    }

    public void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        m_gameIsPause = true;
    }

    public void SettingsOpen()
    {
        SettingsMenuUI.SetActive(true);
    }

    public void SettingsClose()
    {
        SettingsMenuUI.SetActive(false);
    }

    public void QuitGame()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("SavedScene", currentSceneIndex);
        PlayerPrefs.SetInt("Score", gm.score);
        PlayerPrefs.SetInt("Lives", pc.lives);
        PlayerPrefs.SetInt("Jumps", pc.maxNumberOfJumps);
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadMain()
    {
        SceneManager.LoadScene("MainMenu");
        PlayerPrefs.DeleteKey("SavedScene");
        PlayerPrefs.DeleteKey("Score");
        PlayerPrefs.DeleteKey("Lives");
        PlayerPrefs.DeleteKey("Jumps");
    }
}
