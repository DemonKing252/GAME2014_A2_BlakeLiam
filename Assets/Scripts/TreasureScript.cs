using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
File: TreasureScript.cs
Author: Liam Blake
Created: 2020-11-16
Modified: 2020-11-29
*/
public class TreasureScript : MonoBehaviour
{
    [SerializeField]
    GameObject abilityUI;

    Ability ability;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("hello");
        // choose a random potion to be stored in this chest (equips immediately after picking up)
        int rand = Random.Range(0, 3);
        switch (rand)
        {
            case 0:
                ability.healthPotions++;
                break;
            case 1:
                ability.freezePotions++;
                break;
            case 2:
                ability.speedPotions++;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<PlayerController>().GetComponentInChildren<BoxCollider2D>().IsTouching(GetComponent<BoxCollider2D>()))
        {
            FindObjectOfType<PlayerController>().pickup.Play();
            
            FindObjectOfType<PlayerController>().ability.healthPotions += ability.healthPotions;
            FindObjectOfType<PlayerController>().ability.freezePotions += ability.freezePotions;
            FindObjectOfType<PlayerController>().ability.speedPotions += ability.speedPotions;

            // add time to potion
            if (FindObjectOfType<PlayerController>().ability.healthPotions > 0)
            {
                FindObjectOfType<PlayerController>().ability.healthPotions = 0;
                abilityUI.GetComponent<AbilityController>().healthTimeRemaining += 10.0f;
            }

            if (FindObjectOfType<PlayerController>().ability.freezePotions > 0)
            {
                abilityUI.GetComponent<AbilityController>().freezeTimeRemaining += 10.0f;
                FindObjectOfType<PlayerController>().ability.freezePotions = 0;
            }
            
            if (FindObjectOfType<PlayerController>().ability.speedPotions > 0)
            {
                abilityUI.GetComponent<AbilityController>().speedTimeRemaining += 10.0f;
                FindObjectOfType<PlayerController>().ability.speedPotions = 0;
            }


            
            abilityUI.GetComponent<AbilityController>().UpdateGUI();

            Destroy(gameObject);
        }
    }

}
