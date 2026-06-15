
using System.Collections;
using TMPro;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TV : MonoBehaviour
{
    public static TV Instance { get; private set; }

    [Header("UI")]
    public TextMeshProUGUI moviePercent; //шкала заполненности фильма

    public Animator blinkAnimator;
    public Animator Player;
    
    public int cutSceneIndex;
    [Header("Настройки")]
    //активный стресс

    public float activeStressSpeed = 1f;

    public float monsterSpawnVariable = 1f;
    public float eyeSpawnVariable = 10f;

    public int eyeCount = 4;
    //пассивный стресс

    public float stressMax = 0f;
    public float stressSpeed = 1f;

    //шкалы фильма

    public float maxMovie = 100f;
    public float movieSpeed = 1f;



    [HideInInspector]
    public bool monsterSpawned = false;
    [HideInInspector]
    public bool eyeSpawned = false;
    [HideInInspector]
    public int despawnEyeCalls = 0;
    private bool MouseOnSprite = false;
    private bool isWatchingTV = false;
    //увеличивающиеся переменные
    private float _activeStressVariable = 0f;
    //private float _passiveStressVariable = 0f;
    private float _movieVariable = 0f;

    public static int _awakeCount = 0;
    public static int _corridorCount = 0;

    private Camera _mainCamera;

    public System.Action OnMonsterSpawn;

    public System.Action OnEyeSpawn;

    public Canvas crosshair;

    public AudioClip whisper;
    public AudioClip tvNoise;

    public AudioSource sub1, sub2, sub3, sub4, sub5, sub6, sub7, sub8, sub9, sub10;
    public AudioSource cutsceneAudio;
    private AudioSource audioSource;
    private AudioSource tvAudioSource;

    public CinemachineCamera _cam;

    void Awake()
    {
        // Проверяем, не создан ли уже такой объект
        if (Instance == null)
        {
            Instance = this;
           
        }
        
    }

    void Start()
    {
        _mainCamera = Camera.main;
        _movieVariable = 0;
        monsterSpawned = false;
        audioSource = GetComponent<AudioSource>();
        tvAudioSource = gameObject.AddComponent<AudioSource>();
        tvAudioSource.loop = true;
        blinkAnimator.SetTrigger("openEye");

        if (ExitDoor.isEnd)
        {
            StartCoroutine(CutScene());
            ExitDoor.isEnd = false;  // Сбрасываем, чтобы не повторялось
        }
        else
        {
            StartCoroutine(AwakeSubtitles());
            Player.SetLayerWeight(cutSceneIndex, 0f);
        }

    }

    void Update()
    {
        //проверка смотрения телевизора
        if (_mainCamera != null && Mouse.current != null)
        {
            // Получаем позицию мыши 
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Ray ray = _mainCamera.ScreenPointToRay(mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 1000f))
            {
                MouseOnSprite = hit.collider.gameObject == gameObject;
            }
            else
            {
                MouseOnSprite = false;
            }
        }


        //рост шкалы фильма
        if (MouseOnSprite)
        {

            // Включаем звук при начале просмотра
            if (!isWatchingTV)
            {
                tvAudioSource.clip = tvNoise;
                tvAudioSource.loop = true;  
                tvAudioSource.Play();

                isWatchingTV = true;
            }

            // Сохраняем старое значение для проверки
            float oldMovieValue = _movieVariable;

            _movieVariable += movieSpeed * Time.deltaTime;
            _movieVariable = Mathf.Clamp(_movieVariable, 0f, maxMovie);//ограничение шкалы фильма

            
            if (_movieVariable >= monsterSpawnVariable && !monsterSpawned)
            {
                SpawnMonster();
                monsterSpawnVariable = float.MaxValue;
            }

            if (_movieVariable >= eyeSpawnVariable && !eyeSpawned)
            {
                SpawnEye();
                eyeSpawnVariable = float.MaxValue;
            }
            
        }
        else 
        {
            if (isWatchingTV)
            {
               tvAudioSource.Stop();  // Останавливаем TV шум
                isWatchingTV = false;
            }

        }

        if (monsterSpawned)
        {
            _activeStressVariable += activeStressSpeed * Time.deltaTime;
            _activeStressVariable = Mathf.Clamp(_activeStressVariable, 0f, stressMax);
            
        }
        if (eyeSpawned)
        {
            _activeStressVariable += activeStressSpeed * Time.deltaTime;
            _activeStressVariable = Mathf.Clamp(_activeStressVariable, 0f, stressMax);
        }
        
        UpdateMovieBar();

        if (_activeStressVariable >= stressMax)
        {
            RecursionOfGame();
        }
              


    }

    private void SpawnMonster()
    {
        monsterSpawned = true;
        OnMonsterSpawn?.Invoke();
        StartCoroutine(monsterSpawnedSubtitles());

    }
    public void DespawnMonster()
    {
        monsterSpawned = false;
        eyeSpawned = false;
        _activeStressVariable = 0f;
        StartCoroutine(monsterDespawnedSubtitles());
    }

    private void SpawnEye()
    {
        eyeSpawned = true;
        OnEyeSpawn?.Invoke();
        audioSource.PlayOneShot(whisper);
        StartCoroutine(eyeSpawnedSubtitles());
    }
    public void DespawnEye()
    {
        despawnEyeCalls++;
        if (despawnEyeCalls >= eyeCount)
        {
            monsterSpawned = false;
            eyeSpawned = false;
            _activeStressVariable = 0f;
            StartCoroutine(eyeDespawnedAnim());
            _corridorCount++;
        }
    }
    IEnumerator AwakeSubtitles()
    {
        if (_awakeCount == 0)
        {
            yield return new WaitForSeconds(1f);
            sub1.Play();
            subtitles.instance.ShowSubtitle("я уснула? мы же только что фильм смотрели", 5);
            yield return new WaitForSeconds(5f);
/*
            sub2.Play();
            subtitles.instance.ShowSubtitle("тело как будто парализовало", 5f);
            yield return new WaitForSeconds(5f);*/
            sub3.Play();
            subtitles.instance.ShowSubtitle("только головой и могу вертеть, и почему телевизор не выключен?", 6f);

        }
        else
        {
            
            yield return new WaitForSeconds(3f);
            sub4.Play();
            subtitles.instance.ShowSubtitle("я.. где-то ошиблась?", 5);
        }
        
    }

    IEnumerator monsterSpawnedSubtitles()
    {
        if (_awakeCount == 0)
        {
            yield return new WaitForSeconds(1f);
            sub5.Play();
            
            subtitles.instance.ShowSubtitle("я не буду смотреть на него, а он на меня, договорились?", 5);
        }
        else
        {
            
            yield return new WaitForSeconds(1f);
            sub6.Play();
            subtitles.instance.ShowSubtitle("во второй раз ты уже даже не такой страшный", 5);
        }
       
    }
    IEnumerator monsterDespawnedSubtitles()
    {
        if (_awakeCount == 0)
        {
            
            yield return new WaitForSeconds(1f);
            sub7.Play();
            subtitles.instance.ShowSubtitle("исчез.. видать нужно было все-таки смотреть", 5);
        }
        else
        {
            
            yield return new WaitForSeconds(1f);
            sub8.Play();
            subtitles.instance.ShowSubtitle("надеюсь мы больше не увидимся", 5);
        }

    }
    IEnumerator eyeSpawnedSubtitles()
    {
        
        if (_awakeCount == 0)
        {
            
            yield return new WaitForSeconds(1f);
            sub9.Play();
            subtitles.instance.ShowSubtitle("мне уже не смешно, что происходит", 4);

        }
        else
        {
            
            yield return new WaitForSeconds(1f);
            sub10.Play();
            subtitles.instance.ShowSubtitle("...", 5);
        }
     }
    IEnumerator eyeDespawnedAnim()
    {
        blinkAnimator.Play("closeEyes");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(2);
    }
    IEnumerator CutScene()
    {
        StopCoroutine(AwakeSubtitles());
        _mainCamera = null;
        crosshair.enabled = false;
        
        //_cam.enabled = false;
        if (_cam.TryGetComponent<CinemachineInputAxisController>(out var inputProvider))
            inputProvider.enabled = false;
        yield return new WaitForSeconds(1f);
        
        Player.Play("Cutscene");
        cutsceneAudio.Play();
        yield return new WaitForSeconds(30f);
        SceneManager.LoadScene(0);

    }

    public void RecursionOfGame()
    {
        _awakeCount ++;
        SceneManager.LoadScene(1);
        
    }
    void UpdateMovieBar()
    {
        moviePercent.text = "M" + _movieVariable.ToString("F1") + "s"  + "as" + _activeStressVariable.ToString("F1");
      
    }


}


