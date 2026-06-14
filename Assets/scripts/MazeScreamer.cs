using UnityEngine;
using System.Collections;

public class MazeScreamer : MonoBehaviour
{
    public int speed = 1;
    public Transform Player;
    public AudioClip steps;
    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
        }
    }
    void StepsAudio()
    {
        audioSource.PlayOneShot(steps);
    }
    IEnumerator Screamer()
    {
        yield return new WaitForSeconds(1f);

    }
}
