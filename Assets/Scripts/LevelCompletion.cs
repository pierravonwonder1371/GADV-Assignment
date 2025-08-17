using UnityEngine;

public class LevelCompletion : MonoBehaviour
{
    [SerializeField] private GameObject completionScreen; // assign in Inspector
    [SerializeField] private MonoBehaviour playerControls; // your player movement script
    private bool completed = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player touched the flag
        if (collision.CompareTag("Interactable"))
        {
            completed = true;
        }

        if (completed)
        {
            // Disable player input
            if (playerControls != null)
                playerControls.enabled = false;

            // Show completion screen
            if (completionScreen != null)
                completionScreen.SetActive(true);

            Debug.Log("Level Completed!");
        }
    }
}
//Made with assistance from ChatGPT. ChatGPT was asked to provide improvements to the code to make it more efficient.
//Additional assistance from Gemini. The prompt is the same.