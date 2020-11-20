using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseSceneScript : MonoBehaviour
{

    [SerializeField]
    AudioSource theme, btnClick;

    //
    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(3040, 1440, false);
        theme.Play();
        if (Utilities.scenesChanged == 0)
        {
            SceneManager.LoadScene(0);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PlayAgain()
    {
        btnClick.Play();
        Utilities.scenesChanged++;
        SceneManager.LoadScene(3);
    }
    public void BackToMenu()
    {
        btnClick.Play();
        Utilities.scenesChanged++;
        SceneManager.LoadScene(0);
    }
}
