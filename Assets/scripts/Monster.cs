using UnityEngine;


public class LookToDestroy : MonoBehaviour
{
    public GameObject LookToDestroyPrefab;
    void Start()
    {
       LookToDestroyPrefab.SetActive(false);

        TV moviePercent = FindFirstObjectByType<TV>();

        if (moviePercent != null)
        {
            // Подписываемся на событие
            moviePercent.SpawnMonster += Spawn;

            // Также проверяем, не достигло ли уже значение 10 до этого момента
            if (moviePercent.MovieVarieble >= 10f)
            {
                Spawn();
            }
        }

    }

    void Spawn()
    {
        LookToDestroyPrefab.SetActive(true);
    }
   
}
