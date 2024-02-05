using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    public Rigidbody playerRigidbody;
    public bool isGrounded;
    public float speedCount;
    public bool jumping;

    [Header("Speed values")]
    public float speed;
    public float airSpeed;

    public float jumpSpeed;
    public float jumpSideSpeed;

    public float jumpResetTime;
    private float jumpResetCounter;

    public float gravity;
    public float gravityMultiplier;


    [Header("Player Animation")]
    public PlayerAnimations playerAnimations;
    public GameObject playerModel;

    [Header("Sound")]
    public AudioSource walkingSound;



    private void Start()
    {
        
    }

    void Update()
    {
        //horizontal movement, gebruik het nieuwe systeem.
        speedCount = Input.GetAxisRaw("Horizontal");

        if (isGrounded)
        {
            transform.Translate(Vector3.right * speedCount * speed * Time.deltaTime);
        }

        else
        {
            playerRigidbody.AddForce(Vector3.right * speedCount * airSpeed * Time.deltaTime, ForceMode.Impulse);
        }

        //gravity
        playerRigidbody.AddForce(-Vector3.up * gravity * Time.deltaTime);
        if(isGrounded == false)
        {
            gravity += gravityMultiplier * Time.deltaTime;
        }

        else
        {
            gravity = 0;
        }

        //avoid double jump
        jumpResetCounter += Time.deltaTime;

        //jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && GetComponent<WallJumping>().onWall == false && jumpResetCounter > jumpResetTime)
        {
            jumping = true;
        }

        playerAnimations.isJumping = jumping;


        //slope rotation
        int groundLayerMask = 1 << LayerMask.NameToLayer("Ground");
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1, groundLayerMask))
        {
            // Your existing code to calculate slope angle and rotate the player
            float slopeAngle = Vector3.Angle(hit.normal, Vector3.up) * Mathf.Sign(hit.normal.x);

            //float clampedRotation = Mathf.Clamp(slopeAngle, -45, 45);

            Quaternion targetRotation = Quaternion.Euler(0f, 0f, -slopeAngle);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 30);
        }

        else
        {
            Vector3 targetEulerAngles = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
            Quaternion targetRotation = Quaternion.Euler(targetEulerAngles);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 30);
        }


        //sound (UX)
        if (isGrounded && speedCount != 0 && walkingSound.isPlaying == false)
        {
            walkingSound.Play();
        }

        else
        {
            walkingSound.Pause();
        }
    }

    private void FixedUpdate()
    {
        
        if(jumping)
        {

            jumpResetCounter = 0;
            playerRigidbody.AddForce(Vector3.up * jumpSpeed * Time.deltaTime);

            float jumpSpeedCount = Input.GetAxis("Horizontal");
            playerRigidbody.AddForce(transform.right * jumpSideSpeed * jumpSpeedCount * Time.deltaTime);

            jumping = false;
        }
    }
}
