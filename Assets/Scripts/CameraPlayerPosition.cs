using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraPlayerPosition : MonoBehaviour
{
    public Transform playerPosition;
    public Transform currentLevelCam;

    private Vector3 lastPosition;
    private float timeSinceLastMove;

    public float movementThreshold = 0.01f;

    public float timeThreshold = 1.0f;
    public float transitionSpeed;

    public bool transitioningCamToPlayer;

    void Start()
    {
        lastPosition = playerPosition.position;
        transitioningCamToPlayer = true;
    }

    void Update()
    {
        if (Vector3.Distance(playerPosition.position, lastPosition) > movementThreshold)
        {
            timeSinceLastMove = 0.0f;
            
        }
        else
        {
           
            timeSinceLastMove += Time.deltaTime;

            if (timeSinceLastMove >= timeThreshold)
            {
                
                transform.position = Vector3.Lerp(transform.position, currentLevelCam.position, transitionSpeed * Time.deltaTime);
                transitioningCamToPlayer = true;
            }
        }

        lastPosition = playerPosition.position;

        if(timeSinceLastMove <= timeThreshold)
        {
            if(transitioningCamToPlayer)
            {
                transform.position = Vector3.Lerp(transform.position, playerPosition.position - new Vector3(0, 0, 10), transitionSpeed * Time.deltaTime);

                if(transform.position == playerPosition.position - new Vector3(0, 0, 10))
                {
                    transitioningCamToPlayer = false;
                }
            }
        }
    }

     
    
}
