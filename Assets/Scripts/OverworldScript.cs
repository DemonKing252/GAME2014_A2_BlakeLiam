﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*
File: OverworldScript.cs
Author: Liam Blake
Created: 2020-11-26
Modified: 2020-11-30
*/
public class OverworldScript : MonoBehaviour
{
    [SerializeField]
    AudioSource btnClick;
    /* NOTE: Temporary variables to prove that each scene works */

    public void LoadLoseScene()
    {
        btnClick.Play();
        Utilities.scenesChanged++;
        SceneManager.LoadScene(4);
    }
    public void LoadWinScene()
    {
        btnClick.Play();
        Utilities.scenesChanged++;
        SceneManager.LoadScene(5);
    }
}
