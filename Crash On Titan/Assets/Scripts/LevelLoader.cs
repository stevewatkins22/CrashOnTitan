using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    public GameManager gm;
    public PlayerController pc;

    public float transitionTime = 1;

    public int value = 100;

    private void Start()
    {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        PlayerPrefs.SetInt("Score", gm.score);
        PlayerPrefs.SetInt("Lives", pc.lives);
        PlayerPrefs.SetInt("Jumps", pc.numberOfJumps);

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }

    private void AddScore()
    {
        gm.AddScore(value);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionGameObject = collision.gameObject;

        if (collisionGameObject.name == "Player") 
        {
            AddScore();
            LoadNextLevel();
        }
        else
        {
            Debug.Log("Not a player");
        }
    }
}
