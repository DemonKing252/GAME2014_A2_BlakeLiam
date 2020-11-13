using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HowToPlayController : MonoBehaviour
{
    [SerializeField]
    AudioSource theme, btnClick;

    // Start is called before the first frame update
    void Start()
    {
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
    public void BackToMenu()
    {
        btnClick.Play();
        Utilities.scenesChanged++;
        SceneManager.LoadScene(0);
    }
}
