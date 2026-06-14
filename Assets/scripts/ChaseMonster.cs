using UnityEngine;
using UnityEngine.SceneManagement;
public class ChaseMonster : MonoBehaviour
{
    public float speed = 2f;
    private bool hasTriggered = false;
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            TriggerAction();
        }
    }

    private void TriggerAction()
    {
        TV._awakeCount++;
        SceneManager.LoadScene(1);


    }

}
