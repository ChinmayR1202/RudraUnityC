using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPickup : MonoBehaviour
{
    [SerializeField] private Transform objectDetect;
    [SerializeField] private Transform playerPickUp;
    [SerializeField] private float detectDist;
    [SerializeField] private float objDetectRadius;
    [SerializeField] private LayerMask whatIsObj;
    [SerializeField] private LayerMask whatCanBeDetected;
    private bool isPickedUp;


    private void Start()
    {
        isPickedUp = false;
    }

    private void Update()
    {
        
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(objectDetect.position, objDetectRadius, whatIsObj);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                Vector3 dirToTarget = (gameObject.transform.position - transform.position).normalized;                    
                RaycastHit2D detectObject = Physics2D.Raycast(objectDetect.position, dirToTarget, detectDist,whatCanBeDetected);
                
                if (detectObject.collider != null && detectObject.collider.CompareTag("uncollidable object pickup - Lamp") == true)
                {
                    Physics2D.IgnoreCollision(GetComponent<Collider2D>(), detectObject.collider.gameObject.GetComponent<Collider2D>());

                    if (Input.GetKeyDown("f"))
                    {
                        if (!isPickedUp)
                        {

                            detectObject.collider.gameObject.transform.parent = playerPickUp;
                            detectObject.collider.gameObject.transform.position = playerPickUp.position;
                            detectObject.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                            isPickedUp = true;
                        }

                        else
                        {
                            detectObject.collider.gameObject.transform.parent = null;
                            detectObject.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;

                            isPickedUp = false;

                        }

                    }
                }

                if (detectObject.collider != null && detectObject.collider.CompareTag("collidable object pickup") == true)
                {

                    if (Input.GetKeyDown("f"))
                    {
                        if (!isPickedUp)
                        {

                            detectObject.collider.gameObject.transform.parent = playerPickUp;
                            detectObject.collider.gameObject.transform.position = playerPickUp.position;
                            detectObject.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                            isPickedUp = true;
                        }

                        else
                        {
                            detectObject.collider.gameObject.transform.parent = null;
                            detectObject.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;

                            isPickedUp = false;

                        }

                    }


                }

                

        }

        }


    }

 

}
