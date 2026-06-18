using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public int menuScene;
    public int gameScene;

    public AudioClip bgm;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();        
        audioSource.clip = bgm;
        audioSource.loop = true;
        audioSource.Play();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }
    public void ToGame()
    {
        SceneManager.LoadScene(gameScene);
    }
    public void ExitGame()
    {
        Application.Quit();
    }


}
