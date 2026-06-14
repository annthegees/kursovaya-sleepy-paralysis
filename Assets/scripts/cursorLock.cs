using UnityEngine;

public class chsrcntrk : MonoBehaviour
{
    public Animator Tv;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void Tvoff()
    {
        Tv.SetTrigger("off");
    }
    public void Tvon()
    {
        Tv.SetTrigger("on");
    }
}
