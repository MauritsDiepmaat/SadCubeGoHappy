using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTransition : MonoBehaviour
{
    public GameObject player;
    public GameObject cam;
    public CameraPlayerPosition cinemachineCam;

    public Transform[] nextPlayerPositions;
    public Transform[] nextCameraPositions;

    public Image blackBackground;
    public float fadeDuration;
    public float delayDuration = 1f;

    private int i;

    private void Update()
    {
        //devtool
        if(Input.GetKeyDown(KeyCode.T))
        {
            //change position player
            player.transform.position = nextPlayerPositions[i].position;
            cam.transform.position = nextCameraPositions[i].position;
            cinemachineCam.currentLevelCam = nextCameraPositions[i];
            i++;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            StartCoroutine(FadeImage());
        }
    }

    IEnumerator FadeImage()
    {
        // Get the current alpha value of the image
        float currentAlpha = blackBackground.color.a;

        // Target alpha value (e.g., fully opaque)
        float targetAlpha = 1f;

        // Time elapsed
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            // Interpolate the alpha value smoothly
            float newAlpha = Mathf.Lerp(currentAlpha, targetAlpha, elapsedTime / fadeDuration);

            // Update the image's color with the new alpha value
            blackBackground.color = new Color(blackBackground.color.r, blackBackground.color.g, blackBackground.color.b, newAlpha);

            // Increment the elapsed time
            elapsedTime += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Ensure the image is fully opaque
        blackBackground.color = new Color(blackBackground.color.r, blackBackground.color.g, blackBackground.color.b, targetAlpha);

        //change position player
        player.transform.position = nextPlayerPositions[i].position;
        cam.transform.position = nextCameraPositions[i].position;
        cinemachineCam.currentLevelCam = nextCameraPositions[i];
        i++;

        // Wait for the specified delay
        yield return new WaitForSeconds(delayDuration);

        // Start the fade-out process
        StartCoroutine(FadeOutImage());
    }

    IEnumerator FadeOutImage()
    {
        // Get the current alpha value of the image
        float currentAlpha = blackBackground.color.a;

        // Target alpha value (fully transparent)
        float targetAlpha = 0f;

        // Time elapsed
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            // Interpolate the alpha value smoothly
            float newAlpha = Mathf.Lerp(currentAlpha, targetAlpha, elapsedTime / fadeDuration);

            // Update the image's color with the new alpha value
            blackBackground.color = new Color(blackBackground.color.r, blackBackground.color.g, blackBackground.color.b, newAlpha);

            // Increment the elapsed time
            elapsedTime += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Ensure the image is fully transparent
        blackBackground.color = new Color(blackBackground.color.r, blackBackground.color.g, blackBackground.color.b, targetAlpha);
    }
}
