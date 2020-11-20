using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    [SerializeField]
    AudioSource theme, btnClick;

    //
    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(3040, 1440, false);
        theme.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGame()
    {
        btnClick.Play();
        Utilities.scenesChanged++;
        SceneManager.LoadScene(2);
    }
    public void HowToPlay()
    {
        btnClick.Play();
        Utilities.scenesChanged++;
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        btnClick.Play();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
