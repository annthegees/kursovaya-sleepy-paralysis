using UnityEngine;
using UnityEngine.InputSystem;
using System;
using TMPro;

public class TV : MonoBehaviour
{
    public TextMeshProUGUI moviePercent; //шкала заполненности фильма

    public float StressVarieble = 0f;
    public float MovieVarieble = 0f;
    public float MaxMovie = 100f;
    public float StressSpeed = 1f;
    public float MovieSpeed = 1f;
   

    //переменные для уставших глаз от долгого ппрсмотра
    public float tieredVarieble = 5f; //сколько секунд на тв надо для усталости
    public int spacePressesRequired = 3;

    private bool MouseOnSprite = false;
    private Camera mainCamera;

    private float tieredTimer = 0f;
    private bool isTiered = false; //устал тлт нет
    private int currentSpacePresses = 0;
    private bool monsterSpawned = false;

    public static event Action StartBlink;//для моргания
    public static event Action StopBlink;

    public System.Action SpawnMonster;


    void Start()
    {
        mainCamera = Camera.main;

        MovieVarieble = 0;
        UpdateMovieBar();
        monsterSpawned = false;
    }

    void Update()
    {
        //нажатие лкм для снятия усталости
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (isTiered)
            {
                currentSpacePresses++;


                if (currentSpacePresses >= spacePressesRequired)
                {
                    isTiered = false;
                    currentSpacePresses = 0;
                    tieredTimer = 0f;

                }
            }
        }

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
            if (!isTiered) //растет просмотр и усталость
            {
                StopBlink?.Invoke();//передача остановки моргания
               
                tieredTimer += Time.deltaTime;

                // Сохраняем старое значение для проверки
                float oldMovieValue = MovieVarieble;

                MovieVarieble += MovieSpeed * Time.deltaTime;
                MovieVarieble = Mathf.Clamp(MovieVarieble, 0f, MaxMovie);//ограничение шкалы фильма

                UpdateMovieBar();

                Debug.Log("movie is up: " + MovieVarieble);

                if (MovieVarieble >= 10f && !monsterSpawned)
                {
                    monsterSpawned = true; // Ставим флаг, чтобы не спавнить повторно
                    SpawnMonster?.Invoke(); // Вызываем событие спавна монстра
                    
                }

                if (tieredTimer >= tieredVarieble) //заблокирован просмотр от усталости
                {
                    isTiered = true;
                    currentSpacePresses = 0;
                    StartBlink?.Invoke(); //передача в скрипт моргания

                }
            }
            else //растет стресс после блокировки просмотра
            {
                StressVarieble += StressSpeed * Time.deltaTime;
                Debug.Log("stress is up: " + StressVarieble);
            }

        }

        else //растет стресс когда не смотрю тв
        {
            if (!isTiered) // обнуляет усталость если отвернуться до блокировки 
            {
                tieredTimer = 0f;
                StopBlink?.Invoke();//передача остановки моргания
            }


            StressVarieble += StressSpeed * Time.deltaTime;
            Debug.Log("stress is up: " + StressVarieble);
        }




    }
    //заполнение шкалы на экране
    void UpdateMovieBar()
    {
        moviePercent.text = "Movie: " + MovieVarieble.ToString("F1");
    }
}
