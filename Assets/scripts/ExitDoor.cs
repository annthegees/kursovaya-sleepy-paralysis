using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Unity.Cinemachine;

public class ExitDoor : MonoBehaviour
{
    public static ExitDoor Instance;
    public CharCntrkV2 playerController;
    public static bool isEnd = false;
    public CinemachineCamera _cam;
    public GameObject screamer;
    
    void Awake()
    {
        Instance = this;
        screamer.SetActive(false);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isEnd = true;
            screamer.SetActive(true);
            StartCoroutine(End());
        }
    }
    IEnumerator End()
    {
        

        playerController.enabled = false;
        if (_cam.TryGetComponent<CinemachineInputAxisController>(out var inputProvider))
            inputProvider.enabled = false;
        yield return new WaitForSeconds(1000f);
        SceneManager.LoadScene(1);
        
    }
}
