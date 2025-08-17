using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    private bool isGameOver = false;

    void Start()
    {
        gameOverPanel.SetActive(false);
    }

    void Update()
    {

        if (isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                print("Application Quit");
                Application.Quit();
            }
        }
    }

    private IEnumerator GameOverSequence()
    {
        // Show main "Game Over" text immediately
        gameOverPanel.SetActive(true);

        // Wait 1 second
        yield return new WaitForSeconds(1f);
    }

    public void TriggerGameOver()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            gameOverPanel.SetActive(true);
            Debug.Log("Game Over Screen Shown");
            StartCoroutine(GameOverSequence());
        }
    }
}
//Made with assistance from ChatGPT. ChatGPT was asked to provide improvements to the code to make it more efficient.