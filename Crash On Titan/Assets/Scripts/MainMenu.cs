using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    private int sceneToContinue;

    public int setLives = 3;
    public int setJumps = 2;

    public GameObject SettingMenuUI;

    private void Start()
    {
        SettingMenuUI.SetActive(false);
    }

    public void Continue()
    {
        sceneToContinue = PlayerPrefs.GetInt("SavedScene");
        if(sceneToContinue!=0)
        {
            SceneManager.LoadScene(sceneToContinue);
        }
        else
        {
            New();
        }
    }

    public void New()
    {
        PlayerPrefs.DeleteKey("Score");
        PlayerPrefs.DeleteKey("Retry");
        PlayerPrefs.SetInt("Lives", setLives);
        PlayerPrefs.SetInt("Jumps", setJumps);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }

    public void NewGame()
    {
        PlayerPrefs.DeleteKey("Score");
        PlayerPrefs.DeleteKey("Retry");
        PlayerPrefs.SetInt("Lives", setLives);
        PlayerPrefs.SetInt("Jumps", setJumps);
        SceneManager.LoadScene("Level_1");
    }
    
    public void SettingsOpen()
    {
        SettingMenuUI.SetActive(true);
    }

    public void SettingsClose()
    {
        SettingMenuUI.SetActive(false);
    }
}
