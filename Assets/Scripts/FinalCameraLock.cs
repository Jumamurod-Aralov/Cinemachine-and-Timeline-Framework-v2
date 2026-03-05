using UnityEngine;
using Cinemachine;

public class FinalCameraLock : MonoBehaviour
{
    public CinemachineVirtualCamera finalCamera;

    public CinemachineVirtualCamera[] otherCameras;

    public void ActivateFinalCamera()
    {
        // Disable all other cameras
        foreach (var cam in otherCameras)
        {
            if (cam != null)
                cam.Priority = 0;
        }

        // Force this camera
        finalCamera.Priority = 100;
    }
}