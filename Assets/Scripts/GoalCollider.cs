using System.Collections;
using UnityEngine;

public class GoalCollider : MonoBehaviour
{
    public Vector3 targetPosition; // Target position for the player to teleport to
    public float teleportDelay = 1.0f; // Delay before teleporting the player

    private bool cubeInside = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("SadCube"))
        {
            cubeInside = true;
            StartCoroutine(StartTeleportCountdown());
            Debug.Log("Cube entered the trigger zone.");
        }
    }

    private IEnumerator StartTeleportCountdown()
    {
        yield return new WaitForSeconds(teleportDelay);

        if (cubeInside)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                player.transform.position = targetPosition;
                Debug.Log("Player teleported.");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("SadCube"))
        {
            cubeInside = false;
            Debug.Log("Cube exited the trigger zone.");
        }
    }
}
