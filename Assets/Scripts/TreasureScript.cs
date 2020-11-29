using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TreasureScript : MonoBehaviour
{
    [SerializeField]
    GameObject abilityUI;

    Ability ability;

    // Start is called before the first frame update
    void Start()
    {
        int rand = Random.Range(0, 2);
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
            Debug.Log("yes");
            Debug.Log(ability.healthPotions.ToString());

            FindObjectOfType<PlayerController>().ability.healthPotions += ability.healthPotions;
            FindObjectOfType<PlayerController>().ability.freezePotions += ability.freezePotions;
            FindObjectOfType<PlayerController>().ability.speedPotions += ability.speedPotions;

            if (FindObjectOfType<PlayerController>().ability.healthPotions > 0)
                abilityUI.GetComponent<AbilityController>().healthTimeRemaining += 10.0f;

            if (FindObjectOfType<PlayerController>().ability.freezePotions > 0)
                abilityUI.GetComponent<AbilityController>().freezeTimeRemaining += 10.0f;

            if (FindObjectOfType<PlayerController>().ability.speedPotions > 0)
                abilityUI.GetComponent<AbilityController>().speedTimeRemaining += 10.0f;


            
            abilityUI.GetComponent<AbilityController>().UpdateGUI();

            Destroy(gameObject);
        }
    }

}
