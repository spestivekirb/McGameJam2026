using UnityEngine;
using System.Collections; 

public class SpeedPortal : MonoBehaviour

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
            shadow_script.ChangeDelay(delay + 0.5f);
            Debug.Log("Speed Portal");
            Debug.Log(shadow_script.GetDelay());
            StartCoroutine(FreezeDelay());
        }
       
    }

    private IEnumerator FreezeDelay()
    {
        yield return new WaitForSeconds(shadow_script.GetDelay());
        shadow_script.FreezeShadow(0.5f);

    }
}
