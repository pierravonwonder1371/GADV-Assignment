using UnityEngine;

public class LevelCompletion : MonoBehaviour
{
    [SerializeField] private GameObject completionScreen; // assign in Inspector
    [SerializeField] private MonoBehaviour playerControls; // your player movement script
    private bool completed = false;

    void Start()
    {
        /*Similar to the game over screen, the completion screen initally has
         SetActive to false so that it does not appear immediately.*/
        completionScreen.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*This code is used for the completion flag. The flag is tagged "Interactable",
         so when the player touches it, completed's internal flag is raised, becoming true.
         To prevent players from continuing to move on the completion screen, their controls
         are disabled as soon as completed becomes true. Also, the completion screen is made
         visible by setting SetActive to true.*/

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
            
            //The console is updated to indicate that the completion screen has shown up as intended.
            Debug.Log("Level Completed!");
        }
    }
}
//Made with assistance from ChatGPT. ChatGPT was asked to provide improvements to the code to make it more efficient.
//Additional assistance from Gemini. The prompt is the same.
//Altered from this tutorial by Antonio Delgado: https://gt3000.medium.com/game-over-man-creating-a-game-over-screen-in-unity-90e1be71cd85