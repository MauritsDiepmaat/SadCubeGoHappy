using UnityEngine;

public class StageCompletionZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the thrown object is the cube (adjust the tag as needed)
        if (other.CompareTag("SadCube"))
        {
            // Perform actions for completing the stage
            Debug.Log("Stage completed! You can now move on to the next one.");

            // Add any additional logic or scene transitioning code here

            // Optionally, you can reset the cube or disable it
            other.gameObject.SetActive(false);
        }
    }
}
