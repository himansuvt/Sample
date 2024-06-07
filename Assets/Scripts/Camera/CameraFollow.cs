using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Transform fpsView; // Assign this in the Inspector
    public float smoothing = 5f;

    private Vector3 offset;
    private bool isFPV = false; // Flag to check if we are in first-person view
    private Camera cam;
    private PlayerMovement playerMovement;
    private Vector3 thirdPersonCamPosition;
    private Quaternion thirdPersonCamRotation;

    void Start()
    {
        offset = transform.position - player.position;
        cam = GetComponent<Camera>();
        playerMovement = player.GetComponent<PlayerMovement>();

        // Ensure the camera starts in orthographic mode
        cam.orthographic = true;

        // Store the initial position and rotation of the camera
        thirdPersonCamPosition = transform.position;
        thirdPersonCamRotation = transform.rotation;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            isFPV = !isFPV;
            playerMovement.isFPV = isFPV; // Inform the PlayerMovement script about the view mode

            if (isFPV)
            {
                // Switch to perspective view
                cam.orthographic = false;

                // Store the current camera position and rotation before switching
                thirdPersonCamPosition = transform.position;
                thirdPersonCamRotation = transform.rotation;
            }
            else
            {
                // Switch to orthographic view
                cam.orthographic = true;
                ResetCameraPosition();
            }
        }

        if (isFPV)
        {
            HandleFPVRotation();
        }
    }

    void FixedUpdate()
    {
        if (isFPV)
        {
            // Switch to first-person view
            transform.position = fpsView.position;
            transform.rotation = fpsView.rotation;
        }
        else
        {
            // Third-person view
            Vector3 targetCamPos = player.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
        }
    }

    void HandleFPVRotation()
    {
        float horizontal = Input.GetAxis("Mouse X");
        float vertical = Input.GetAxis("Mouse Y");

        player.Rotate(0, horizontal, 0);
        transform.Rotate(-vertical, 0, 0);
    }

    void ResetCameraPosition()
    {
        // Reset the camera's position and rotation to the stored third-person view position and rotation
        transform.position = thirdPersonCamPosition;
        transform.rotation = thirdPersonCamRotation;
    }
}
