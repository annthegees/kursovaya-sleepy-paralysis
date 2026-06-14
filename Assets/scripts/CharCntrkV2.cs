using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using static Unity.Collections.AllocatorManager;
using System.Collections;
public class CharCntrkV2 : MonoBehaviour
{
    public float speed;
    public CharacterController _charCntrl;
    public CinemachineCamera _cam;
    public GameObject flashlight;
    public Animator blinkAnimator;

    public AudioSource sub11, sub12, sub13;

    //private bool isFlashlightOn = false;

    private Vector2 _move;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        blinkAnimator.Play("openEye");
        StartCoroutine(startSubtitles());
    }
    public void OnMove(InputValue val)
    {
        _move = val.Get<Vector2>();

    }
    private void Update()
    {
        _charCntrl.Move((GetForward() * _move.y + GetRight() * _move.x) * Time.deltaTime * speed);
        bool isMoving = _move.magnitude > 0.1f /*&& _charCntrl.isGrounded*/;

        if (isMoving)
        {
            CMshake.Instance.ShakeCamera(2f, 0.5f);
        }
        else
        {
            CMshake.Instance.ShakeCamera(0f, .0f);
        }
        
    }
    private Vector3 GetForward()
    {
        Vector3 forward = _cam.transform.forward;
        forward.y = 0f;

        return forward.normalized;
    }

    private Vector3 GetRight()
    {
        Vector3 right = _cam.transform.right;
        right.y = 0f;
        return right.normalized;
    }
    IEnumerator startSubtitles()
    {
        yield return new WaitForSeconds(3f);
        sub11.Play();
        subtitles.instance.ShowSubtitle("это когда нибудь вообще закончиться?", 5);
        yield return new WaitForSeconds(5f);
        sub12.Play();
        subtitles.instance.ShowSubtitle("темно что глаза вырви, может у меня фонарик есть?", 5);
        yield return new WaitForSeconds(6f);
        flashlight.SetActive(true);
        yield return new WaitForSeconds(2f);
        sub13.Play();
        subtitles.instance.ShowSubtitle("серьезно что-ли", 5);
    }
}
