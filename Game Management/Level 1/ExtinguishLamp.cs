using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ExtinguishLamp : MonoBehaviour
{

    [SerializeField] private Light2D lamp;

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("uncollidable object pickup - Lamp"))
        {
            if (lamp.enabled == true)
            {
                lamp.enabled = false;
            }

        }
    }
}
