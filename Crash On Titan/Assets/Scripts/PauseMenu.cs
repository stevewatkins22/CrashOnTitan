using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool m_gameIsPause = false;
    public GameObject PauseMenuUI;
    public GameObject SettingsMenuUI;

    private void Start()
    {
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
        SceneManager.LoadScene("MainMenu");
    }
}
