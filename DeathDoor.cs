using UnityEngine;

public class DeathDoor : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object we touched is tagged as "Player"
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<Playermove>();
            if (player != null)
            {
                player.die();
                Debug.Log("Pillar crushed the player!");
            }
        }
    }
}
