using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


public class LookToDestroy : MonoBehaviour
{
    /*public GameObject LookToDestroyPrefab;

    public Animator animator;

    public float minDelay;  // Минимальная задержка (секунд)
    public float maxDelay;  // Максимальная задержка (секунд)
    

    private Coroutine handCoroutine;

    private Camera mainCamera;

    private bool MouseOnSprite = false;

    void Start()
    {
        mainCamera = Camera.main;

        LookToDestroyPrefab.SetActive(false);

        TV moviePercent = FindFirstObjectByType<TV>();

        if (moviePercent != null)
        {
            // Подписываемся на событие
            moviePercent.OnMonsterSpawn += Spawn;

            // Также проверяем, не достигло ли уже значение 10 до этого момента
            if (moviePercent._movieVariable >= 10f)
            {
                Spawn();
            }
        }

    }

    private void Update()
    {
        if (mainCamera != null && Mouse.current != null)
        {
            // Получаем позицию мыши 
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Ray ray = mainCamera.ScreenPointToRay(mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 1000f))
            {
                MouseOnSprite = hit.collider.gameObject == gameObject;
            }
            else
            {
                MouseOnSprite = false;
            }
        }

        if (MouseOnSprite)
        {
            Debug.Log("on sprite");
            animator.Rebind();
        }
    }

    void Spawn()
    {
        LookToDestroyPrefab.SetActive(true);

        if (handCoroutine != null) return;
        handCoroutine = StartCoroutine(coroutineHandStart());

    }
    IEnumerator coroutineHandStart()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
            animator.SetTrigger("handStart");
        }
    }

    public void HandFinish()
    {
        Debug.Log("Анимация закончилась");
        TV stressPercent = FindFirstObjectByType<TV>();
        stressPercent._passiveStressVariable += 20f; //урон от монстра

    }


    */
}
