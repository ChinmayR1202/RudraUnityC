using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningAxe : MonoBehaviour
{

    // Rotation
    [SerializeField] private float axeRotationAcc;
    [SerializeField] private float axeRotationSpeed;
    private float iniAxeRotSpeed;


    [SerializeField] private GameObject axe;


    private bool freefall;
    public bool isRotationStop;
    public bool reverseRotation;


    // Start is called before the first frame update
    void Start()
    {
        iniAxeRotSpeed = axeRotationSpeed;
        isRotationStop = false;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isRotationStop)
        {
            Rotate();
        }



        if (axe.transform.rotation.z < 0)
        {
            if (!reverseRotation)
            {
                freefall = false;
            }
            else
            {
                freefall = true;
            }

        }
        if (axe.transform.rotation.z >= 0)
        {
            if (!reverseRotation)
            {
                freefall = true;
            }
            else
            {
                freefall = false;
            }

        }
    }

    private void Rotate()
    {
        if (!freefall)
        {
            if (!reverseRotation)
            {
                axe.transform.Rotate(new Vector3(0, 0, 10) * axeRotationSpeed * Time.deltaTime);
            }
            else
            {
                axe.transform.Rotate(new Vector3(0, 0, -10) * axeRotationSpeed * Time.deltaTime);
            }
            axeRotationSpeed = iniAxeRotSpeed;
        }
        else
        {
            if (!reverseRotation)
            {
                axe.transform.Rotate(new Vector3(0, 0, 10) * axeRotationSpeed * Time.deltaTime);
            }
            else
            {
                axe.transform.Rotate(new Vector3(0, 0, -10) * axeRotationSpeed * Time.deltaTime);
            }

            axeRotationSpeed += axeRotationAcc * Time.deltaTime;

        }
    }


}
