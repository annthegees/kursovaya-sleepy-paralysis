using UnityEngine;
using UnityEngine.SceneManagement;

public class CorridorEnd : MonoBehaviour
{
    private bool hasTriggered = false;
   
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player") && !hasTriggered)
        {
            TriggerAction();
        }
    }

    private void TriggerAction()
    {
        
        SceneManager.LoadScene(3);


    }
}
