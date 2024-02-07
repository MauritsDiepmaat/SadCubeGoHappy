using UnityEngine;

public class CubeAnimationController : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        // Get the Animator component attached to the cube
        animator = GetComponent<Animator>();
    }

    // Method to play the happy animation
    public void PlayHappyAnimation()
    {
        // Trigger the "Happy" animation state
        animator.SetBool("Happy",true);
    }

    // Method to play the sad animation
    public void PlaySadAnimation()
    {
        // Trigger the "Sad" animation state
        animator.SetBool("Happy",false);
    }
}
