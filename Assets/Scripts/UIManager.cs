using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private GameObject gameOverScreen;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        gameOverScreen.SetActive(false); // make sure it's hidden at start
    }

    public void ShowGameOver()
    {
        gameOverScreen.SetActive(true);
        Time.timeScale = 0f; // pause game
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex
        );
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
