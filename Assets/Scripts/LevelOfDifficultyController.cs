using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelOfDifficultyController : MonoBehaviour
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
    public void Easy()
    {
        btnClick.Play();
        Utilities.diff = Difficulty.Easy;
        SceneManager.LoadScene(3);
    }
    public void Normal()
    {
        btnClick.Play();
        Utilities.diff = Difficulty.Normal;
        SceneManager.LoadScene(3);
    }
    public void Hard()
    {
        btnClick.Play();
        Utilities.diff = Difficulty.Hard;
        SceneManager.LoadScene(3);
    }
}
