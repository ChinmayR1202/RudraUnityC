using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private Transform cutscene;
    [SerializeField] private CinemachineVirtualCamera cameraAttatched;
    [SerializeField] private GameObject player;
    [SerializeField] private float cutsceneDuration;
    [SerializeField] private float shakeDuration;
    [SerializeField] private float pauseDuration;
    [SerializeField] [Range(0, 0.5f)] private float xPosMinRange;
    [SerializeField] [Range(0, 0.5f)] private float yPosMinRange;
    [SerializeField] [Range(0, 0.5f)] private float smooth;

    [SerializeField] private PlayerMovement playerMovement;

    private Vector2 currentPos;
    private Vector2 initialPos;
    private Vector3 velocity = Vector3.zero;

    private float shakeIntensityX;
    private float shakeIntensityY;
    private float timer;

    private bool isCameraShake;
    private bool isCutsceneDone;
    private bool isCutsceneOver;
    private bool isDoneOnce;
    private bool startTimer;

    private void Start()
    {
        initialPos = cameraAttatched.transform.position;
        currentPos = cameraAttatched.transform.position;
        isCameraShake = true;
        isCutsceneDone = true;
        isCutsceneOver = false;
        isDoneOnce = false;
        timer = 0;
    }

    // Start is called before the first frame update
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            timer = 0;
            cameraAttatched.Follow = player.transform;
            startTimer = false;
        }
        
    }

    private void Update()
    {
        if (startTimer)
        {
            timer += Time.deltaTime;
        }


        if (!isCutsceneDone)
        {
            playerMovement.isMovementLocked = true;


            if (timer > pauseDuration && timer <= (shakeDuration + pauseDuration))
            {
                CameraShake();
            }
            
            if (timer > (shakeDuration + pauseDuration) && timer <= (shakeDuration + pauseDuration + cutsceneDuration))
            {
                isCameraShake = false;
                DoCutScene();
            }
            
            if(timer > (shakeDuration + pauseDuration + cutsceneDuration))
            {

                cameraAttatched.transform.position = Vector3.SmoothDamp(cameraAttatched.transform.position, initialPos, ref velocity, smooth);
                isCutsceneDone = true;
                playerMovement.isMovementLocked = false;
                isDoneOnce = true;
            }
            
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            cameraAttatched.Follow = null;

            if (!isDoneOnce)
            {
                if (Input.GetButton("Interact"))
                {
                    Debug.Log("Pressing Down cutscene");
                    isCutsceneDone = false;
                    isCutsceneOver = false;
                    startTimer = true;
                }
            }
            

        }
    }

    private void CameraShake()
    {

        if (isCameraShake)
        {
            shakeIntensityX = Random.Range((xPosMinRange * -1f), xPosMinRange);
            shakeIntensityY = Random.Range((yPosMinRange * -1f), yPosMinRange);
            currentPos.x += shakeIntensityX;
            currentPos.y += shakeIntensityY;
            cameraAttatched.transform.position = Vector3.SmoothDamp(initialPos, currentPos, ref velocity, smooth);
            currentPos = initialPos;
            //cameraAttatched.transform.position = initialPos;
        }
        
    }

    private void DoCutScene()
    {
        if (!isCutsceneOver)
        {
            cameraAttatched.transform.position = Vector3.SmoothDamp(initialPos, cutscene.transform.position, ref velocity, smooth);
            //cameraAttatched.transform.position = initialPos;

            isCutsceneOver = true;
        }      
    }
    
}
