using UnityEngine;
using Cinemachine;

public class CameraSwap : MonoBehaviour
{
    public CinemachineVirtualCamera cockpitCam;
    public CinemachineVirtualCamera externalCam;

    private bool isCockpitActive = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SwapCamera();
        }
    }

    void SwapCamera()
    {
        if (isCockpitActive)
        {
            cockpitCam.Priority = 10;
            externalCam.Priority = 20;
        }
        else
        {
            cockpitCam.Priority = 20;
            externalCam.Priority = 10;
        }

        isCockpitActive = !isCockpitActive;
    }
}