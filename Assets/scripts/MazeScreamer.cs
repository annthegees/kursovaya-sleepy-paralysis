using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MazeScreamer : MonoBehaviour
{
    public int speed = 1;
    public Transform Player;
    public AudioClip steps;
    public AudioSource scream;
    public Image screamerImage;
    private AudioSource audioSource;
    private void Awake()
    {
        screamerImage.enabled = false;
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        screamerImage.enabled = false;
        StepsAudio();
    }
    private void Update()
    {
        if (ExitDoor.isEnd == true)
        {
            Vector3 direction = (Player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
            
            // StartCoroutine(StepsSound());
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Screamer());
            Debug.Log("aaaa");
        }
    }
    void StepsAudio()
    {
        audioSource.PlayOneShot(steps);
    }
    IEnumerator Screamer()
    {
        
        yield return new WaitForSeconds(3f);
        scream.Play();
        screamerImage.enabled = true;
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(1);
    }
}
