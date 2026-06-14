using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
public class CharacterControl : MonoBehaviour
{
    public float stepDistance = 1f;

    private CharacterController controller;
    private float lastStepTime = -999f;
    private Camera mainCamera;
    public Animator blinkAnimator;

    public AudioSource sub15, sub16;
    void Start()
    {
        mainCamera = Camera.main; 
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        StartCoroutine(StartSubtitles());
        blinkAnimator.Play("openEye");
    }

    void Update()
    {
        if (Keyboard.current.wKey.wasPressedThisFrame && Time.time >= lastStepTime)
        {
            TakeStep();
        }
    }

    void TakeStep()
    {
        // Движение вперед (по направлению камеры)
        Vector3 moveDirection = transform.forward * stepDistance;
        controller.Move(moveDirection);
    }
    IEnumerator StartSubtitles()
    {
        if (TV._corridorCount <= 1)
        {
            
            yield return new WaitForSeconds(0f);
            sub15.Play();
            subtitles.instance.ShowSubtitle("я все еще сплю", 3);

            yield return new WaitForSeconds(3f);
            sub16.Play();
            subtitles.instance.ShowSubtitle("теперь могу ползти? надо поторопиться, мне кажется я слышу шаги сзади", 5);
        }
        else
        {
            yield return new WaitForSeconds(2f);
            sub15.Play();
            subtitles.instance.ShowSubtitle("я все еще сплю", 5);
        }
    }

}