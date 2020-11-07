using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelOfDifficultyController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
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
        Utilities.diff = Difficulty.Easy;
        SceneManager.LoadScene(3);
    }
    public void Normal()
    {
        Utilities.diff = Difficulty.Normal;
        SceneManager.LoadScene(3);
    }
    public void Hard()
    {
        Utilities.diff = Difficulty.Hard;
        SceneManager.LoadScene(3);
    }
}
