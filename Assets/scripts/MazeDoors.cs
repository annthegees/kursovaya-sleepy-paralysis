using System.Collections;
using UnityEngine;

public class MazeDoors : MonoBehaviour
{
    public Transform[] teleportPoints;

    private CharacterController playerController;

    

    private int _tpCount = 0;
    private void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {

        // Проверяем, что вошёл игрок 
        if (other.CompareTag("Player") )
        {
            playerController = other.GetComponent<CharacterController>();

            Transform targetPoint = teleportPoints[Random.Range(0, teleportPoints.Length)];
            if (playerController != null)
            {
                playerController.enabled = false;

            }

            other.transform.position = targetPoint.position;

            if (playerController != null)
            {
                playerController.enabled = true;

            }
            _tpCount++;
            StartCoroutine(startSubtitles());
        }


    }
    IEnumerator startSubtitles()
    {
        if (_tpCount == 1)
        {
            yield return new WaitForSeconds(1f);
            
            subtitles.instance.ShowSubtitle("где я?", 4);
        }
        else
        {
            yield return new WaitForSeconds(1f);
            subtitles.instance.ShowSubtitle("...", 2);
        }
        }
    }
