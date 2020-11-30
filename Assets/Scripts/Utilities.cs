using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Difficulty
{
    Easy = 0,
    Normal = 1,
    Hard = 2
}


public class Utilities
{
    public static Difficulty diff;
    public static int scenesChanged = 0;

    public static float[] damageFactor = { 0.5f, 1.0f, 1.5f };

    public static int score = 0;
    public static int kills = 0;

}
