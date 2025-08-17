using UnityEngine;

public class PlayerConditions : MonoBehaviour
{
    private Transform currentCheckpoint;
    private Health playerHealth;
    private UIManager uiManager;

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