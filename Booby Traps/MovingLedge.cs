using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingLedge : MonoBehaviour
{
    private float turnTime;
    [SerializeField] private Transform top;
    [SerializeField] private Transform bottom;
    [SerializeField] private float switchTime;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float leverNumber;
    [SerializeField] private bool isLedgeMovingRight;
    [SerializeField] private bool isLedgeMovingUp;
    [SerializeField] private bool isMoveAuto;
    [SerializeField] private bool isMoveByTme;

    // Start is called before the first frame update
    void Start()
    {
        turnTime = 0f;
        if (!isMoveAuto)
        {
            GameEvents.moveLedge.OnMoveLedges += MoveTheLedge;
        }
        
    }

    private void FixedUpdate()
    {
        if (isMoveAuto)
        {
            MoveTheLedge();
        }
    }

    private void SwitchDir()
    {
        moveSpeed *= -1f;
    }

    public void MoveTheLedge()
    {
        if (isLedgeMovingRight)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);

            if (isMoveByTme)
            {
                turnTime += Time.deltaTime;

                if (turnTime >= switchTime)
                {
                    SwitchDir();
                    turnTime = 0f;
                }
            }
            else
            {
                if(transform.position.x == top.position.x)
                {
                    SwitchDir();
                }
                if (transform.position.x == bottom.position.x)
                {
                    SwitchDir();
                }
            }
            

            

        }

        if (isLedgeMovingUp)
        {


            transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);

            if (isMoveByTme)
            {
                turnTime += Time.deltaTime;

                if (turnTime >= switchTime)
                {
                    SwitchDir();
                    turnTime = 0f;
                }
            }
            else
            {
                if (transform.position.y == top.position.y)
                {
                    SwitchDir();
                }
                if (transform.position.y == bottom.position.y)
                {
                    SwitchDir();
                }
            }



        }
    }
}
