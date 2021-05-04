using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceLedge : MonoBehaviour
{
    [SerializeField] private float bobbleSpeed;
    [SerializeField] private float switchTime;
    private float timer;
    private float endTime;
    private bool isBobbleDone;
    private bool startTimer;


    // Start is called before the first frame update
    void Start()
    {
        endTime = switchTime * 2;
        startTimer = false;
        isBobbleDone = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            startTimer = true;
            isBobbleDone = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isBobbleDone = true;
            startTimer = false;
            timer = 0; 
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (!isBobbleDone)
        {
            if (startTimer)
            {
                timer += Time.deltaTime;
            }

            if(timer < switchTime)
            {
                transform.Translate(Vector2.down * bobbleSpeed * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector2.up * bobbleSpeed * Time.deltaTime);
            }

            if(timer >= endTime)
            {
                transform.Translate(Vector2.up * 0 * Time.deltaTime);
                startTimer = false;
                isBobbleDone = true;
            }


        }
        


        
    }
}
