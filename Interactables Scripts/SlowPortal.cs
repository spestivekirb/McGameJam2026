using UnityEngine;

public class SlowPortal : MonoBehaviour

{
    public shadowFollower shadow_script;

    public Playermove player_script;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        shadow_script = Object.FindAnyObjectByType<shadowFollower>();
    }



    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            float delay = shadow_script.GetDelay();
            shadow_script.ChangeDelay(delay - 0.5f);

          
            player_script.FreezePlayer(0.5f);
            Debug.Log(shadow_script.GetDelay());
            Debug.Log("Slow Portal");
        }
       
    }
}
