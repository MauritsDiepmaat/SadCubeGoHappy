using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    public LayerMask groundLayer;

    private Rigidbody rb;
    private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Check if the player is grounded
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1f, groundLayer);

        // Handle player movement
        MovePlayer();

        // Handle player jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
    }

    private void MovePlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(horizontalInput * speed, rb.velocity.y, 0f);
        rb.velocity = movement;
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, jumpForce, 0f);
    }
}
