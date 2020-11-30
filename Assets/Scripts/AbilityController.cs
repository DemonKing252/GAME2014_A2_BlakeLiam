using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum AbilityType
{
    Health,
    Freeze,
    Speed
}
public struct Ability
{
    public int healthPotions;

    public int freezePotions;

    public int speedPotions;
    public void Zero()
    {
        healthPotions = 0;
        freezePotions = 0;
        speedPotions = 0;
    }
}

public class AbilityController : MonoBehaviour
{
    public float healthTimeRemaining = 0.0f;
    public float freezeTimeRemaining = 0.0f;
    public float speedTimeRemaining = 0.0f;

    [SerializeField]
    public List<TextMesh> texts;

    // Start is called before the first frame update
    void Start()
    {
        healthTimeRemaining = freezeTimeRemaining = speedTimeRemaining = 0.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        healthTimeRemaining -= Time.deltaTime;
        freezeTimeRemaining -= Time.deltaTime;
        speedTimeRemaining -= Time.deltaTime;

        if (healthTimeRemaining < 0.0f)
        {
            FindObjectOfType<PlayerController>().ability.healthPotions = 0;
            healthTimeRemaining = 0.0f;
        }

        if (freezeTimeRemaining < 0.0f)
        {
            freezeTimeRemaining = 0.0f;
            FindObjectOfType<PlayerController>().ability.freezePotions = 0;
        }

        if (speedTimeRemaining < 0.0f)
        {
            speedTimeRemaining = 0.0f;
            FindObjectOfType<PlayerController>().ability.speedPotions = 0;

        }
        UpdateGUI();
    }

    public void UpdateGUI()
    {
        if (texts.Count == 3)
        {
            texts[0].text = healthTimeRemaining.ToString("F0") + "s";
            texts[1].text = freezeTimeRemaining.ToString("F0") + "s";
            texts[2].text = speedTimeRemaining.ToString("F0") + "s";
        }
    }
}
