using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] monster;
    public Transform[] spawnPoint;

    private int randMonster;
    private int randPosition;
    public float startTimeBtwSpawn;
    private float timeBtwSpawn;


    void Start()
    {
        timeBtwSpawn = startTimeBtwSpawn;
    }

    void Update()
    {
        if (timeBtwSpawn <= 0)
        {
            randPosition = GetFreeSpawnPoint();

            if (randPosition != -1)
            {
                randMonster = Random.Range(0, monster.Length);
                Instantiate(monster[randMonster], spawnPoint[randPosition].transform.position, Quaternion.identity);
            }

            timeBtwSpawn = startTimeBtwSpawn;
        }
        else
        {
            timeBtwSpawn -= Time.deltaTime;
        }

    }
    int GetFreeSpawnPoint()
    {
        // Создаем список свободных точек
        System.Collections.Generic.List<int> freePoints = new System.Collections.Generic.List<int>();

        for (int i = 0; i < spawnPoint.Length; i++)
        {
            // Проверяем, есть ли коллайдер в радиусе точки спавна
            Collider[] hitColliders = Physics.OverlapSphere(spawnPoint[i].position, 1f);
            bool isOccupied = false;

            foreach (var hitCollider in hitColliders)
            {
                // Проверяем, что это монстр (нужжно добавить тег)
                if (hitCollider.CompareTag("Monster"))
                {
                    isOccupied = true;
                    break;
                }
            }

            if (!isOccupied)
            {
                freePoints.Add(i);
            }
        }

        // Если есть свободные точки, возвращаем случайную
        if (freePoints.Count > 0)
        {
            return freePoints[Random.Range(0, freePoints.Count)];
        }

        return -1; // Нет свободных точек
    }
}
