using UnityEngine;

public class CubePickupHandler : MonoBehaviour
{
    public Vector3 objectOffset; // Public variable to adjust the object's position offset
    private GameObject player;

    private void Start()
    {
        // Find the player GameObject using its tag
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        // If the cube is being held by the player, adjust its position relative to the player
        if (transform.parent == player.transform)
        {
            // Adjust the position of the cube relative to the player
            transform.localPosition = objectOffset;
        }
    }
}
