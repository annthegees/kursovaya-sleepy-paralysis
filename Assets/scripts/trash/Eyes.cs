using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class Eyes : MonoBehaviour
{
    [HideInInspector] public int eyeIndex;
    [HideInInspector] public EyeManager manager;

    private Image myImage;
    private bool isPlaying = false;

    void Start()
    {
        myImage = GetComponent<Image>();
    }

    public void OnPointerEnter()
    {
        if (isPlaying) return;

        StartCoroutine(PlayAndDisappear());
    }

    private IEnumerator PlayAndDisappear()
    {
        isPlaying = true;

        yield return new WaitForSeconds(1f);

        myImage.enabled = false;

        //manager.MonsterDefeated(eyeIndex);

        GetComponent<Collider>().enabled = false;
    }
}
