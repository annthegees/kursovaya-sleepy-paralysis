using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class EyeManager: MonoBehaviour
{
    public Image eye;
    public CanvasGroup vignette;

    // Длительность появления виньетки
    private float fadeDuration = 2f;
    private float _vignetteTimer = 0f;


    public float monsterMax;

    private float _monsterVariable;


    private Camera mainCamera;

    private bool MouseOnSprite = false;
    private TV tvReference; // Сохраняем ссылку на TV

    public AudioClip popping;
    
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        mainCamera = Camera.main;
        gameObject.SetActive(false);
        vignette.alpha = 0f;
        
        //подписка на спавн
        if (TV.Instance != null)
        {
            TV.Instance.OnEyeSpawn += ActivateMonster;
            tvReference = TV.Instance; // Сохраняем ссылку
        }
        
    }
    void ActivateMonster()
    {
        _vignetteTimer = 0f;
        _monsterVariable = 0f;
        gameObject.SetActive(true);
        

    }
    private void Update()
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
        }

        if (_monsterVariable >= monsterMax)
        {
            if (tvReference != null)
            {
                tvReference.DespawnEye(); // Сбрасываем флаг в TV
            }

            MouseOnSprite = false;
            gameObject.SetActive(false);

            audioSource.PlayOneShot(popping);

            vignette.alpha = 0f;
        }
    }
   
   

    private void OnDisable()
    {
        if (TV.Instance != null)
        {
            TV.Instance.OnEyeSpawn -= ActivateMonster;
        }
    }
}
