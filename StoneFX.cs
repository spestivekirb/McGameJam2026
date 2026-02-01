using UnityEngine;

public class DoorController : MonoBehaviour
{
    public ParticleSystem stoneParticles;
    private bool hasOpened = false;

    public void OpenDoor()
    {
        if (!hasOpened)
        {
            // Play the particles
            stoneParticles.Play();
            
            hasOpened = true;
        }
    }
}