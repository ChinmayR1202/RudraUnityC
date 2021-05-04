using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonolithInteract : MonoBehaviour
{
    [SerializeField] private bool isAutoTrigger;
    private Rigidbody2D player;
    private bool isTriggerDone;
    private bool isStopped;

    public ReadMonolith readMonolith;
    private bool isInteracting;

    private void Start()
    {
        isTriggerDone = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && Input.GetButton("Interact_Monolith") && !isAutoTrigger)
        {
            if (!isInteracting)
            {
                FindObjectOfType<MonolithScriptManager>().StartReading(readMonolith);
                isInteracting = true;
            }
  
        }

        if (collision.CompareTag("Player") && isAutoTrigger)
        {
            if (!isTriggerDone)
            {
                if (!isStopped)
                {
                    player = collision.GetComponent<Rigidbody2D>();
                    player.velocity = new Vector2(0,0);
                    isStopped = true;
                }
                FindObjectOfType<MonolithScriptManager>().StartReading(readMonolith);
                isInteracting = true;
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInteracting = false;
            isTriggerDone = true;
            isStopped = false;
        }
    }


}
