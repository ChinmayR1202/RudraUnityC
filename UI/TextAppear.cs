using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextAppear : MonoBehaviour
{
    [SerializeField] private Image palmLeaf;
    [SerializeField] private Text message;

    private bool interact;


    void Update()
    {
        isInteracting();
    }

    private void isInteracting()
    {
        if (Input.GetButtonDown("Interact"))
        {
            interact = true;
        }
        else
        {
            interact = false;
        }
    }



    private void OnTriggerEnter2D(Collider2D mainChar)
    {

        if (mainChar.CompareTag("Player"))
        {
            palmLeaf.enabled = true;
            message.enabled = true;
            Debug.Log("Entered");
                      
        }
    }

     private void OnTriggerExit2D(Collider2D mainChar)
    {
        if (mainChar.CompareTag("Player"))
        {
            palmLeaf.enabled = false;
            message.enabled = false;
            Debug.Log("Exit");
        }
    }



}
