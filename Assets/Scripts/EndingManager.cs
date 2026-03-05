using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using Cinemachine;

public class EndingManager : MonoBehaviour
{
    public GameObject restartButton;

    public CinemachineVirtualCamera gameplayCam;
    public CinemachineVirtualCamera cockpitCam;
    public CinemachineVirtualCamera wideShotCam;

    public FinalCameraLock finalCameraLock;

    public MonoBehaviour cameraSwitcher; // your camera logic script

    private PlayableDirector director;

    void Start()
    {
        director = GetComponent<PlayableDirector>();
        director.stopped += OnTimelineFinished;
    }

    void OnTimelineFinished(PlayableDirector pd)
    {
        if (cameraSwitcher != null)
            cameraSwitcher.enabled = false;

        if (finalCameraLock != null)
            finalCameraLock.ActivateFinalCamera();

        restartButton.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}