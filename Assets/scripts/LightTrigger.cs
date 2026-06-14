using UnityEngine;

public class LightTrigger : MonoBehaviour
{
    public GameObject flashlight;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            flashlight.SetActive(true);
        }
    }
}
