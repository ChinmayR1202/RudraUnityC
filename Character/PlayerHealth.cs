using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{
    // Damage Sources
    [SerializeField] private float darknessDamage;
    public bool isLit;

    // Health
    public float health;
    [SerializeField] private float originalHealth = 100;
    [SerializeField] private float lightHealingHealth;

    // Coin / Indicator
    [SerializeField] [Range(0, 1)] private float alpha;
    [SerializeField] [Range(0, 1)] private float rgb;
    [SerializeField] private float waitDamageOver;
    [SerializeField] private Image coinCenter;
    [SerializeField] private Image coinRing;
    private float timer;
    private bool isCoinShown;

    // Damage
    public bool isDamageTaken;
    private bool isAddedToEvent;

    // Start is called before the first frame update
    void Start()
    {
        health = originalHealth;
        alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {

        GameEvents.lightHealth.playerLit();
        GameEvents.lightHealth.TakingDamage();
        UpdateCoin();
        LightEffect();


        if (health > originalHealth)
        {
            health = originalHealth;
        }
        if (health < 0)
        {
            health = 0;
        }


        if (alpha > 0)
        {
            isCoinShown = true;
        }
        else
        {
            isCoinShown = false;
        }

        coinCenter.color = new Color(rgb, rgb, rgb, 1);
        coinRing.color = new Color(alpha, alpha, alpha, 1);

    }


    private void UpdateCoin()
    {
        if (health <= 0)
        {
            rgb = 0;
           // Debug.Log("Character has died");
        }
        if (health >= 0)
        {
            rgb = health / originalHealth;
        }

        if(health >= originalHealth)
        {
            if (alpha < 1)
            {
                alpha += 0.1f;
            }
            
        }
        else
        {
            if( alpha > 0.5f)
            {
                alpha -= 0.1f;
            }
            
        }
    }


    private void LightEffect()
    {
        if (!isDamageTaken && health < originalHealth && isLit)
        {
            timer += Time.deltaTime;

            if(timer >= waitDamageOver)
            {
                health += lightHealingHealth;      
            }
        }

        if (health >= originalHealth)
        {
            timer = 0;
        }

        if (isLit)
        {
            if (isAddedToEvent)
            {
                GameEvents.takeDamage.OnTakingDamage -= TookDamage;
                isAddedToEvent = false;
            }
        }

        if (!isLit && health > 0)
        {
            health -= darknessDamage;

            if (!isAddedToEvent)
            {
                GameEvents.takeDamage.OnTakingDamage += TookDamage;
                isAddedToEvent = true;
            }
        }

    }

    private void TookDamage()
    {
        Debug.Log("Took Damage");
    }
}
