using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    private bool isGameOver = false;

    /* By default, the Game Over panel is obviously not meant to appear when the game starts.
     So, SetActive() is False, effectively hiding the panel.*/
    void Start()
    {
        gameOverPanel.SetActive(false);
    }

    void Update()
    {
        

        if (isGameOver)
        {
            /*isGameOver only returns true when TriggerGameOver is triggered, so you cannot input
             these keys until the screen actually appears. Pressing R restarts the level by refreshing the
             scene, while pressing Esc quits out of the game entirely. The instructions are detailed
             on the Game Over screen.*/
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

    public void TriggerGameOver()
    {
        /*TriggerGameOver is called by the Health script when the player dies, which is called
         when currentHealth reaches zero. Along with that, SetActive becomes true so that the Game Over
         screen becomes visible. isGameOver is set to true so that the player can make selection inputs.*/
        if (!isGameOver)
        {
            isGameOver = true;
            gameOverPanel.SetActive(true);
            //The console is updated to confirm that the code to bring up the screen is working as intended.
            Debug.Log("Game Over Screen Shown");
        }
    }
}
//Made with assistance from ChatGPT. ChatGPT was asked to provide improvements to the code to make it more efficient.
//Based on this tutorial by Antonio Delgado: https://gt3000.medium.com/game-over-man-creating-a-game-over-screen-in-unity-90e1be71cd85