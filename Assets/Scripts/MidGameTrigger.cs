using UnityEngine;
using UnityEngine.Playables;

public class MidGameTrigger : MonoBehaviour
{
    public PlayableDirector director;
    public ShipControls shipControls;

    private bool hasPlayed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasPlayed) return;

        if (other.CompareTag("Player"))
        {
            hasPlayed = true;

            shipControls.enabled = false;   // Disable movement
            director.Play();
        }
    }
}