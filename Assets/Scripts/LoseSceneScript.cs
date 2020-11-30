using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseSceneScript : MonoBehaviour
{

    [SerializeField]
    AudioSource theme, btnClick;

    [SerializeField]
    Text score, kills;

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
        score.text = "Score: " + Utilities.score.ToString();
        kills.text = "Kills: " + Utilities.kills.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PlayAgain()
    {
        Utilities.score = Utilities.kills = 0;
        btnClick.Play();
        Utilities.scenesChanged++;
        SceneManager.LoadScene(3);
    }
    public void BackToMenu()
    {
        Utilities.score = Utilities.kills = 0;
        btnClick.Play();
        Utilities.scenesChanged++;
        SceneManager.LoadScene(0);
    }
}
