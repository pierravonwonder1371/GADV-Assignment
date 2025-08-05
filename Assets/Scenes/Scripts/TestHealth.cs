using UnityEngine;
using UnityEngine.UI;

public class TestHealth : MonoBehaviour
{

    public HealthBar Health;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Health.LoseHealth();
        }
    }
}
