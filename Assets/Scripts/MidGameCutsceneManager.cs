using UnityEngine;
using UnityEngine.Playables;

public class MidGameCutsceneManager : MonoBehaviour
{
    public ShipControls shipControls;

    private PlayableDirector director;

    void Start()
    {
        director = GetComponent<PlayableDirector>();
        director.stopped += OnTimelineFinished;
    }

    void OnTimelineFinished(PlayableDirector pd)
    {
        shipControls.enabled = true;   // Re-enable control
    }
}