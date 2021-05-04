using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnWatermill : MonoBehaviour
{

    [SerializeField] private float rotationSpeed;
    [SerializeField] private bool reverseSpin;
    private float spinDir;
    public bool startSpinning;

    // Start is called before the first frame update
    void Start()
    {
        startSpinning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!reverseSpin)
        {
            spinDir = 1;
        }
        else
        {
            spinDir = -1f;
        }


        if (startSpinning)
        {
            transform.Rotate(new Vector3(0, 0, 10 * spinDir) * rotationSpeed * Time.deltaTime);
        }
    }
}
