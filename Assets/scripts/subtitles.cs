using TMPro;
using UnityEngine;
using System.Collections;
using System;

public class subtitles : MonoBehaviour
{
    public TextMeshProUGUI subtitleText;
    public float charsDelay = 0.05f;
    public float delayBetweenLines = 1f;

    public Action SpawnMonster;

    void Start()
    {


        // Запускаем диалог
        string[] dialogue = {
            "Тебе нравится фильм?",
            "Может посмотрим его вместе?",
            "ну же, просто смотри на телевизор",
            "Мне же тоже интересно",
            "глянь, шкала заполняется",
            "а если ты отвлечешься?",
            "ой, кажется там кто-то сзади?",
        };

        StartCoroutine(PlayDialogue(dialogue));

    }

    IEnumerator PlayDialogue(string[] lines)
    {
        foreach (string line in lines)
        {
            yield return StartCoroutine(TypeLine(line));
            yield return new WaitForSeconds(delayBetweenLines);
        }

        SpawnMonster?.Invoke();
    }

    IEnumerator TypeLine(string line)
    {
        subtitleText.text = "";

        foreach (char c in line)
        {
            subtitleText.text += c;
            
            yield return new WaitForSeconds(charsDelay);
        }
    }
}
