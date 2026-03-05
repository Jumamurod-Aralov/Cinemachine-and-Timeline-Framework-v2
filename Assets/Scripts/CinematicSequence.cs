using UnityEngine;
using Cinemachine;
using System.Collections;

public class CinematicSequence : MonoBehaviour
{
    public CinemachineVirtualCamera[] playerCameras;   // 3rd person POV, cockpit POV
    public CinemachineVirtualCamera[] cinematicCameras; // Cinematic cameras
    public float shotDuration = 3f;
    public float idleTimeToStart = 5f; // seconds of inactivity before cinematic starts
    public bool loopSequence = true;

    private float idleTimer = 0f;
    private bool isCinematicPlaying = false;
    private int currentPlayerCamIndex = 0;
    private int currentCinematicCamIndex = 0;

    void Start()
    {
        // Activate the first player camera by default
        ActivatePlayerCamera(currentPlayerCamIndex);
    }

    void Update()
    {
        bool anyInput = Input.anyKey || Mathf.Abs(Input.GetAxis("Horizontal")) > 0.01f || Mathf.Abs(Input.GetAxis("Vertical")) > 0.01f ||
        Mathf.Abs(Input.GetAxis("Mouse X")) > 0.01f || Mathf.Abs(Input.GetAxis("Mouse Y")) > 0.01f || Input.GetMouseButton(0) || 
        Input.GetMouseButton(1) || Input.GetMouseButton(2);

        // --- PLAYER CAMERA SWITCHING ---
        if (!isCinematicPlaying && Input.GetKeyDown(KeyCode.C))
        {
            currentPlayerCamIndex = (currentPlayerCamIndex + 1) % playerCameras.Length;
            ActivatePlayerCamera(currentPlayerCamIndex);
            idleTimer = 0f; // reset idle
        }

        // --- IDLE CINEMATIC START ---
        if (anyInput && !Input.GetKeyDown(KeyCode.C))
        {
            idleTimer = 0f;
            if (isCinematicPlaying)
            {
                StopAllCoroutines();
                DeactivateAllCinematicCameras();
                isCinematicPlaying = false;
            }
        }
        else
        {
            idleTimer += Time.deltaTime;

            if (idleTimer >= idleTimeToStart && !isCinematicPlaying)
            {
                StartCoroutine(PlayCinematicSequence());
            }
        }
    }

    // --- CINEMATIC SEQUENCE ---
    IEnumerator PlayCinematicSequence()
    {
        isCinematicPlaying = true;

        do
        {
            for (int i = 0; i < cinematicCameras.Length; i++)
            {
                currentCinematicCamIndex = i;
                ActivateCinematicCamera(cinematicCameras[i]);
                float timer = 0f;

                while (timer < shotDuration)
                {
                    timer += Time.deltaTime;

                    // stop cinematic on player input
                    if (Input.anyKey || Mathf.Abs(Input.GetAxis("Horizontal")) > 0.01f ||
                    Mathf.Abs(Input.GetAxis("Vertical")) > 0.01f || Mathf.Abs(Input.GetAxis("Mouse X")) > 0.01f ||
                    Mathf.Abs(Input.GetAxis("Mouse Y")) > 0.01f || Input.GetMouseButton(0) ||
                    Input.GetMouseButton(1) || Input.GetMouseButton(2) || Input.GetKeyDown(KeyCode.C))
                    {
                        DeactivateAllCinematicCameras();
                        isCinematicPlaying = false;
                        yield break;
                    }

                    yield return null;
                }
            }
        } while (loopSequence);

        DeactivateAllCinematicCameras();
        isCinematicPlaying = false;
    }

    void ActivatePlayerCamera(int index)
    {
        // deactivate all player cameras
        foreach (var cam in playerCameras) cam.Priority = 0;
        playerCameras[index].Priority = 20;
    }

    void ActivateCinematicCamera(CinemachineVirtualCamera cam)
    {
        DeactivateAllCinematicCameras();
        cam.Priority = 20;
    }

    void DeactivateAllCinematicCameras()
    {
        foreach (var cam in cinematicCameras) cam.Priority = 0;
        ActivatePlayerCamera(currentPlayerCamIndex);
    }

    // --- PUBLIC METHODS FOR UI ---
    public bool IsCinematicPlaying()
    {
        return isCinematicPlaying;
    }

    public string CurrentCameraName()
    {
        if (isCinematicPlaying)
            return cinematicCameras[currentCinematicCamIndex].name;
        else
            return playerCameras[currentPlayerCamIndex].name;
    }
}