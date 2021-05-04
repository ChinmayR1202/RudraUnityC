using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopBobbing : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private CharacterController2D charac;
    [SerializeField] private float gravScale;
    private Rigidbody2D playerRigid;
    private float iniGravScale;

    void Start()
    {
        playerRigid = player.GetComponent<Rigidbody2D>();
        iniGravScale = playerRigid.gravityScale;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (charac.m_Grounded)
            {
                playerRigid.gravityScale = gravScale;
            }
            else
            {
                playerRigid.gravityScale = iniGravScale;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerRigid.gravityScale = iniGravScale;
        }
    }

    // Update is called once per frame
    
}
