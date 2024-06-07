using UnityEngine;

public class GunController : MonoBehaviour
{
    public Transform gunTransform; // Reference to the gun transform
    public Transform gunFPVPosition; // Assign this in the Inspector

    void Update()
    {
        if (Camera.main.name == "FirstPersonCamera")
        {
            // Set the gun to the FPV position
            gunTransform.position = gunFPVPosition.position;
            gunTransform.rotation = gunFPVPosition.rotation;
        }
        else
        {
            // Handle third-person gun positioning if necessary
        }
    }
}
