using UnityEngine;

public class LookToDestroy : MonoBehaviour
{
    public GameObject LookToDestroyPrefab;
    void Start()
    {
       LookToDestroyPrefab.SetActive(false);

        subtitles subtitle = FindFirstObjectByType<subtitles>();

        if (subtitle != null)
        {
            subtitle.SpawnMonster += Spawn;
        }
    }

    void Spawn()
    {
        LookToDestroyPrefab.SetActive(true);
    }
   
}
