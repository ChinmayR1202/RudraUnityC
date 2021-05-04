using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


public class StatueLightUp : MonoBehaviour
{

    [SerializeField] private ParticleSystem waterfall;
    [SerializeField] private TurnWatermill turnWatermill;
    [SerializeField] private StatueManager statueManager;
    [SerializeField] private Light2D lampToBeLit;

    private bool isStatueLit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("uncollidable object pickup - Lamp"))
        {
            if (!isStatueLit)
            {
                lampToBeLit.enabled = true;
                turnWatermill.startSpinning = true;
                waterfall.enableEmission = true;
                statueManager.statuesLit += 1;
            }
            isStatueLit = true;
        }
    }
}
