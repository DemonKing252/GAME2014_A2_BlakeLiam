using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
File: ScoreController.cs
Author: Liam Blake
Created: 2020-11-10
Modified: 2020-11-14
*/
public class ScoreController : MonoBehaviour
{
    [SerializeField]
    TextMesh scoreText;

    private float[] scoreArr = { 5.0f, 10.0f, 15.0f };

    [SerializeField]
    public float score = 0.0f;

    public void AddKillToScore()
    {
        score += scoreArr[(int)Utilities.diff];

        scoreText.text = "Score: " + score.ToString("F0");

    }
    public void Reset()
    {
        this.score = 0.0f;
    }
}
