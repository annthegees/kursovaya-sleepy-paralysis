using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BreatheMonster : MonoBehaviour
{
    public Image monster;
    public CanvasGroup vignette;
    public AudioClip breathing;

    public Animator DespawnAnim;
    // Длительность появления виньетки
    private float fadeDuration = 2f;
    private float _vignetteTimer = 0f;


    public float monsterMax;

    private float _monsterVariable;

    private AudioSource audioSource;

    private Camera mainCamera;

    private bool MouseOnSprite = false;

    private TV tvReference; // Сохраняем ссылку на TV

    void Start()
    {
        mainCamera = Camera.main;
        gameObject.SetActive(false);
        vignette.alpha = 0f;
        audioSource = GetComponent<AudioSource>();
        //подписка на спавн
        if (TV.Instance != null)
        {
            TV.Instance.OnMonsterSpawn += ActivateMonster;
            tvReference = TV.Instance; // Сохраняем ссылку
        }
    }

    //появление монстра
    void ActivateMonster()
    {
        _vignetteTimer = 0f;
        _monsterVariable = 0f;
        gameObject.SetActive(true);
        audioSource.PlayOneShot(breathing);
    }

    void Update()
    {
        if (vignette.alpha < 1f && _monsterVariable < monsterMax)
        {
            _vignetteTimer += Time.deltaTime;
            vignette.alpha = Mathf.Clamp01(_vignetteTimer / fadeDuration);
        }

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


        if (MouseOnSprite)
        {
            _monsterVariable += Time.deltaTime;
            DespawnAnim.SetTrigger("shaking");

        }
        else
        {
            DespawnAnim.Play("MonsterIdle");
        }


        if (_monsterVariable >= monsterMax)
        {
            if (tvReference != null)
            {
                tvReference.DespawnMonster(); // Сбрасываем флаг в TV
            }
            MouseOnSprite = false;

            vignette.alpha = 0f;
            gameObject.SetActive(false);
        }

    }

    //отписка если монстра не будет
   
    private void OnDisable()
    {
        if (TV.Instance != null)
        {
            TV.Instance.OnMonsterSpawn -= ActivateMonster;
        }
    }
    
}
