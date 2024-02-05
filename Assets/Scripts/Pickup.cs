using UnityEngine;

public class Pickup : MonoBehaviour
{
    private GameObject heldObject;
    private bool isHoldingObject;
    private Rigidbody playerRigidbody;

    private void Start()
    {
        // Assuming your player's Rigidbody is on the same GameObject as this script
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Check if the "E" key is pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isHoldingObject)
            {
                TryPickUpObject();
            }
            else
            {
                ThrowObject();
            }
        }

        // If an object is held, move it along with the player
        if (isHoldingObject)
        {
            MoveHeldObject();
        }
    }

    private void TryPickUpObject()
    {
        // Raycast to check for objects to pick up
        RaycastHit hit;
        Vector3 raycastDirection = transform.TransformDirection(Vector3.right); // Use Vector3.right for X-axis

        // Debug ray to visualize the raycast
        Debug.DrawRay(transform.position, raycastDirection * 2f, Color.red, 2f);

        if (Physics.Raycast(transform.position, raycastDirection, out hit, 2f))
        {
            // Check if the hit object has a specific tag or component to indicate it's pickable
            if (hit.collider.CompareTag("Pickable"))
            {
                // Perform the pick-up logic
                PickUpObject(hit.collider.gameObject);
            }
            else
            {
                Debug.Log("Hit object is not pickable. Tag: " + hit.collider.tag);
            }
        }
        else
        {
            Debug.Log("No object hit.");
        }
    }

    private void PickUpObject(GameObject objToPickUp)
    {
        // Set the heldObject reference
        heldObject = objToPickUp;
        isHoldingObject = true;

        // Parent the held object to the player for movement
        objToPickUp.transform.SetParent(transform);

        Debug.Log("Picked up: " + objToPickUp.name);
    }

    private void ThrowObject()
    {
        // Unparent the held object
        heldObject.transform.SetParent(null);

        // Apply force with an arc pattern
        Rigidbody objRigidbody = heldObject.GetComponent<Rigidbody>();
        if (objRigidbody != null)
        {
            // Use Vector3.right for X-axis and Vector3.up for the vertical component
            Vector3 throwDirection = transform.TransformDirection(Vector3.right) + transform.TransformDirection(Vector3.up);

            // Consider the player's current vertical velocity (jumping force)
            Vector3 jumpForce = Vector3.up * playerRigidbody.velocity.y;

            // Combine throwing force and jumping force
            Vector3 totalForce = throwDirection.normalized * 5f + jumpForce;



            objRigidbody.velocity = totalForce;
        }

        Debug.Log("Threw: " + heldObject.name);

        // Reset the heldObject reference
        heldObject = null;
        isHoldingObject = false;
    }

    private void MoveHeldObject()
    {
        // Move the held object along with the player
        heldObject.transform.position = transform.position + transform.right * 1.5f;
    }
}
