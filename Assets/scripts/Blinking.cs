using UnityEngine;

public class Blinking : MonoBehaviour
{
    public Animator blink;
    public Animator wakeUp;

    void Start()
    {
        blink.Play("blinking");

        wakeUp.Play("wake up");
    }


}
