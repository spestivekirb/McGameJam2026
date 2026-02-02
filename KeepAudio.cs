using UnityEngine;

public class KeepAudio : MonoBehaviour
{
    public static KeepAudio instance;

    void Awake()
    {
        // Check if a music player already exists
        if (instance == null)
        {
            // If not, THIS is the one. Keep it alive.
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If one already exists, destroy this new one immediately
            Destroy(gameObject);
        }
    }
}