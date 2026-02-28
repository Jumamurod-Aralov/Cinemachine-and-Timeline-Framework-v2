using UnityEngine;
using TMPro; // or use UnityEngine.UI for standard text

public class CinematicUI : MonoBehaviour
{
    public TMP_Text tmpText; // assign your TextMeshPro component
    public CinematicSequence cameraSystem;

    void Update()
    {
        string message = "Player Camera Controls:\n- Switch POV: Press C";

        if (cameraSystem.IsCinematicPlaying())
        {
            message += "\n<Cinematic Camera Active> [DEBUG]";
        }

        message += $"\nCurrent Camera: {cameraSystem.CurrentCameraName()}";

        tmpText.text = message;
    }
}