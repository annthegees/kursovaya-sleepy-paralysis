using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Closet : MonoBehaviour
{
    public TextMeshProUGUI fillPercent;

    public GameObject closet;


    public Animator closetAnim;


    public float minDelay;
    public float maxDelay;
    public float stopAnim;

    private float onSpriteCount;

    private Camera mainCamera;

    private bool MouseOnSprite = false;

    private Coroutine animStart;

    private void Start()
    {
        mainCamera = Camera.main;

        if (animStart != null) return;
        animStart = StartCoroutine(coroutineStart());

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
            onSpriteCount += Time.deltaTime;

            fillPercent.text = onSpriteCount.ToString("F0") + "s";

            if (onSpriteCount >= stopAnim)
            {
                Debug.Log("on sprite");
                closetAnim.Rebind();
                onSpriteCount = 0;
            }
        }
        if (MouseOnSprite == false)
        {
            onSpriteCount = 0;
            fillPercent.text = " ";
        }

    }
    IEnumerator coroutineStart()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
            closetAnim.SetTrigger("closetStart");
        }
    }
}
