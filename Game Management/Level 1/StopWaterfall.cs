using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopWaterfall : MonoBehaviour
{
    private ParticleSystem waterfall;


    // Start is called before the first frame update
    void Start()
    {
        waterfall = GetComponent<ParticleSystem>();
        waterfall.enableEmission = false;
    }

}
