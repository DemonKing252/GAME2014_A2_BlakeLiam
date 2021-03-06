﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/*
File: WinScript.cs
Author: Liam Blake
Created: 2020-11-14
Modified: 2020-11-14
*/
public class WinScript : MonoBehaviour
{
    [SerializeField]
    Text score, kills; 

    [SerializeField]
    AudioSource theme, btnClick;

    //
    // Start is called before the first frame update
    void Start()
    {
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
