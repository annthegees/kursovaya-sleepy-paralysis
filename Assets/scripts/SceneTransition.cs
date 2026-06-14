using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition Instance;
    public Animator blinkAnimator;
    
    void Awake()
    {
        
            Instance = this;
          
        
    }

    public static void TransitionToScene(int sceneName)
    {
        if (Instance != null)
        {
            Instance.StartCoroutine(Instance.Transition(sceneName));
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    IEnumerator Transition(int sceneName)
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        blinkAnimator.SetTrigger("transition");
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene(nextSceneIndex);
        
    }
}
