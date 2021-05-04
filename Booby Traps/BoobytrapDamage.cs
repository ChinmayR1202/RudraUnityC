using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoobytrapDamage : MonoBehaviour
{
    [SerializeField] private float damageAmount;
    [SerializeField] private float damageTimeInterval;
    [SerializeField] private bool isDamageContinuous;

    private float timer;   
    private bool isDamageGiven;
    private bool takeDamage;
    private bool isAddedToEvent;

    [SerializeField] private PlayerHealth playerHealth;



    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            takeDamage = true;
            Debug.Log("Taking Damage");

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            takeDamage = false;
            isDamageGiven = false;

        }
    }


    // Update is called once per frame
    void Update()
    {

        if (!takeDamage)
        {
            isDamageGiven = false;
        }
        else
        {
            if (isDamageContinuous)
            {
                if (!isDamageGiven)
                {
                    Debug.Log("Actually took some damage");
                    playerHealth.health -= damageAmount;
                    isDamageGiven = true;
                }

                timer += Time.deltaTime;

                if (timer >= damageTimeInterval)
                {
                    playerHealth.health -= damageAmount;
                    timer = 0f;
                }
            }
            else
            {
                if (!isDamageGiven)
                {
                    playerHealth.health -= damageAmount;
                    isDamageGiven = true;
                    takeDamage = false;
                }
            }
        }

        if (isDamageGiven && !isAddedToEvent)
        {
            GameEvents.takeDamage.OnTakingDamage += TookDamage;
            isAddedToEvent = true;
        }
        if(!isDamageGiven && isAddedToEvent)
        {
            GameEvents.takeDamage.OnTakingDamage -= TookDamage;
            isAddedToEvent = false;
        }
        
    }

    private void TookDamage()
    {
        Debug.Log("Took Damage");
    }
}
