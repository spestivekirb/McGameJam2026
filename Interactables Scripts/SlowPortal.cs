using UnityEngine;

public class SlowPortal : MonoBehaviour

{
    public shadowFollower shadow_script;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }



    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            float delay = shadow_script.GetDelay();
            shadow_script.ChangeDelay(delay - 0.5f);
            Debug.Log("Slow Portal");
            Debug.Log(shadow_script.GetDelay());
        }
       
    }
}
