using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class subtitles : MonoBehaviour
{
    public static subtitles instance;

    [Header("UI")]
    public TextMeshProUGUI subtitleText;          
    public float defaultDuration = 3f;


    [Header("Настройки")]
    public float fadeInTime = 0.2f;
    public float fadeOutTime = 0.3f;

    
    private Coroutine currentSubtitle;
    

    private void Awake()
    {
        instance = this;
       
    }
    public void ShowSubtitle(string text, float duration = -1f)
    {
        if (currentSubtitle != null)
            StopCoroutine(currentSubtitle);

        if (duration < 0)
            duration = defaultDuration;

        currentSubtitle = StartCoroutine(DisplaySubtitle(text, duration));
    }

    IEnumerator DisplaySubtitle(string text, float duration)
    {
        subtitleText.text = text;

        // Плавное появление
        float timer = 0f;
        while (timer < fadeInTime)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, timer / fadeInTime);
            
            yield return null;
        }

        // Висим на экране
        yield return new WaitForSeconds(duration);

        // Плавное исчезновение
        timer = 0f;
        while (timer < fadeOutTime)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeOutTime);
            yield return null;
        }

        subtitleText.text = "";
        currentSubtitle = null;
    }

    

    IEnumerator HideAfterDelay(float duration)
    {
        yield return new WaitForSeconds(duration);

        float timer = 0f;
        while (timer < fadeOutTime)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeOutTime);
            yield return null;
        }

        subtitleText.text = "";
        currentSubtitle = null;
    }

}
