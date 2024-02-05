using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    public Rigidbody playerRigidbody;
    public bool isGrounded;
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
    public GameObject playerModel;

    [Header("Sound")]
    public AudioSource walkingSound;

    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        // Check if grounded using collider overlap
        isGrounded = Physics.OverlapSphere(transform.position, 1f, LayerMask.GetMask("Ground")).Length > 2; // Adjust the sphere radius as needed

        if (isGrounded && !jumping)
        {
            // Set velocity for smooth movement
            playerRigidbody.velocity = new Vector3(horizontalInput * speed, playerRigidbody.velocity.y, 1f);

            // Jump
            if (Input.GetKeyDown(KeyCode.Space) && jumpResetCounter > jumpResetTime)
            {
                jumping = true;
                playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, CalculateJumpSpeed(), 1f);
            }
        }
        else
        {
            // Add force for air movement
            playerRigidbody.AddForce(Vector3.right * horizontalInput * airSpeed * Time.deltaTime, ForceMode.Impulse);
        }

        // Gravity
        playerRigidbody.AddForce(Vector3.down * gravity * Time.deltaTime);

        // Avoid double jump
        jumpResetCounter += Time.deltaTime;

        // Slope rotation
        int groundLayerMask = 1 << LayerMask.NameToLayer("Ground");
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1, groundLayerMask))
        {
            float slopeAngle = Vector3.Angle(hit.normal, Vector3.up) * Mathf.Sign(hit.normal.x);
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, -slopeAngle);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 30);
        }
        else
        {
            Vector3 targetEulerAngles = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
            Quaternion targetRotation = Quaternion.Euler(targetEulerAngles);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 30);
        }

        // Sound (UX)
        if (isGrounded && Mathf.Abs(horizontalInput) > 0.1f && !walkingSound.isPlaying)
        {
            walkingSound.Play();
        }
        else if (isGrounded && Mathf.Abs(horizontalInput) < 0.1f)
        {
            walkingSound.Pause();
        }
    }

    private float CalculateJumpSpeed()
    {
        return Mathf.Sqrt(2 * gravity * jumpSpeed);
    }

    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (Vector3.Dot(contact.normal, Vector3.up) > 0.5f)
            {
                isGrounded = true;
                jumping = false;
                jumpResetCounter = 0;
                break; // Exit the loop if at least one contact is upwards
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = false;
        }
    }

}
