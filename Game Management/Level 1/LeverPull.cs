using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverPull : MonoBehaviour
{
    [SerializeField] private float leverActionNumber;
    [SerializeField] private float machineAlpha;
    private bool isLeverPulled;
    private bool moveLedges = false;
    [SerializeField] private GameObject waterfall;
    [SerializeField] private GameObject lever;
    [SerializeField] private GameObject machineOn;


    // Start is called before the first frame update
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Entered");


            if (Input.GetButton("Interact"))
            {
                Debug.Log("Interacted");
                isLeverPulled = true;

                switch (leverActionNumber)
                {
                    case 1:
                        waterfall.GetComponent<PolygonCollider2D>().enabled = false;
                        waterfall.GetComponent<ParticleSystem>().enableEmission = false;
                        break;

                    case 2:
                        moveLedges = true;
                        break;

                    case 3:
                        waterfall.GetComponent<PolygonCollider2D>().enabled = false;
                        waterfall.GetComponent<ParticleSystem>().enableEmission = false;
                        moveLedges = true;
                        break;

                }

            }

        }

    }


    private void FixedUpdate()
    {
        if (isLeverPulled)
        {
            changeAlpha();

            if (lever.transform.rotation.z > -0.25f)
            {
                lever.transform.Rotate(new Vector3(0, 0, 10) * -15 * Time.deltaTime);
            }

            if (moveLedges)
            {
                GameEvents.moveLedge.MoveLedges();
            }

        }

    }

    private void changeAlpha()
    {
        machineOn.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, machineAlpha);
        if (machineAlpha < 1f)
        {
            machineAlpha += 0.1f;
        }

    }
}
