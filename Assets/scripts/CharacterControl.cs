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
        if (TV._corridorCount == 1)
        {
            yield return new WaitForSeconds(2f);
            subtitles.instance.ShowSubtitle("я все еще сплю", 5);

            yield return new WaitForSeconds(5f);
            subtitles.instance.ShowSubtitle("я могу ползти? надо поторопиться, мне кажется кто-то идет за мной", 5);
        }
        else
        {
            yield return new WaitForSeconds(2f);
            subtitles.instance.ShowSubtitle("это уже начинает надоедать", 5);
        }
    }

}