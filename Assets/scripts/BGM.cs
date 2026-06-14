using UnityEngine;
using UnityEngine.Audio;

public class BGM : MonoBehaviour
{
    public AudioClip ticking;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = ticking;
        audioSource.loop = true;
        audioSource.Play();
    }

    
}
