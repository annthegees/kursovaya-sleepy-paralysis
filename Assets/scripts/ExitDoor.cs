using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour
{
    public static ExitDoor Instance;

    public static bool isEnd = false;
    void Awake()
    {
        Instance = this;
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isEnd = true;
            SceneManager.LoadScene(1);
            Debug.Log("the end");
        }
    }
}
