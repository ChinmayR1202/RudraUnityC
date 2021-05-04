using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaReducer : MonoBehaviour
{
    [SerializeField] [Range(0, 1)] private float alpha;
    [SerializeField] [Range(0, 1)] private float alphaLim;
    [SerializeField] [Range(0, 1)] private float alphaReduce;
    public GameObject diminishingBG;
    private bool appear;


    private void Start()
    {
        alpha = 1;
        appear = true;
    }

    private void FixedUpdate()
    {
        if (appear)
        {
            if (alpha < 1)
            {
                alpha += alphaReduce;
            }

        }
        else
        {
            if ((alpha - alphaReduce) > alphaLim)
            {
                alpha -= alphaReduce;
            }

        }

        if (alpha > 1)
        {
            alpha = 1;
        }

        if (alpha < 0)
        {
            alpha = 0;
        }

        diminishingBG.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alpha);
    }





    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.CompareTag("Player"))
        {
            appear = false;
        }
    }

    private void OnTriggerExit2D(Collider2D player)
    {
        if (player.CompareTag("Player"))
        {
            //diminishingBG.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            appear = true;
        }
    }

}
