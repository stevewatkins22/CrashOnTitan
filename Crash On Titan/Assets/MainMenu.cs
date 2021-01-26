using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject SettingMenuUI;

    private void Start()
    {
        SettingMenuUI.SetActive(false);
    }

    public void Continue()
    {
        SceneManager.LoadScene("Level_One");
    }

    public void New()
    {
        SceneManager.LoadScene("Level_One");
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
