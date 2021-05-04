using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class GameEvents : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public static GameEvents lightHealth;
    public static GameEvents moveLedge;
    public static GameEvents takeDamage;
    public static GameEvents lightBotStatue;
    public static GameEvents lightTopStatue;

    public float statuesLit;
    // Start is called before the first frame update
    private void Awake()
    {
        lightHealth = this;
        moveLedge = this;
        takeDamage = this;
        lightBotStatue = this;
        lightTopStatue = this;

        statuesLit = 0;
    }

    public event Action OnPlayerLit;
    public void playerLit()
    {
        if (OnPlayerLit != null)
        {
            playerHealth.isLit = true;
        }
        else
        {
            playerHealth.isLit = false;
        }
    }


    public event Action OnMoveLedges;

    public void MoveLedges()
    {
        OnMoveLedges?.Invoke();
    }


    public event Action OnBotStatuesLit;

    public void LightBotStatues()
    {
        if(OnBotStatuesLit != null)
        {
            OnBotStatuesLit?.Invoke();
        }
    }


    public event Action OnTopStatuesLit;

    public void LightTopStatues()
    {
        if (OnTopStatuesLit != null)
        {
            OnTopStatuesLit?.Invoke();
        }
    }

    public event Action OnTakingDamage;

    public void TakingDamage()
    {
        if(OnTakingDamage != null)
        {
            playerHealth.isDamageTaken = true;
        }
        else
        {
            playerHealth.isDamageTaken = false;
        }
    }
}
