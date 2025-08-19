using UnityEngine;

public class PlayerConditions : MonoBehaviour
{
    private Transform currentCheckpoint;
    private Health playerHealth;
    private UIManager uiManager;

    /*This code was not used as it was made for the checkpoints.
     It was not needed, since this is such a short level.*/
    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        uiManager = FindFirstObjectByType<UIManager>();
        currentCheckpoint = new GameObject("StartCheckpoint").transform;
        currentCheckpoint.position = transform.position;
    }

    public void Respawn()
    {
        transform.position = (currentCheckpoint.position);
        playerHealth.Respawn();
    }
}
//Made with assistance from ChatGPT. ChatGPT was asked to provide improvements to the code to make it more efficient.
//Based on this tutorial by Pandemonium: https://www.youtube.com/playlist?list=PLgOEwFbvGm5o8hayFB6skAfa8Z-mw4dPV