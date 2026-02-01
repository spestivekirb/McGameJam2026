using UnityEngine;

public class PillarMover : MonoBehaviour
{
    [Header("Pillar Settings")]
    public float cycleDuration = 3.0f; 
    public float height = 5.0f;
    public float offset = 0.0f;
    [Range(0, 1)]
    public float upTimePercent = 0.85f; 

    [Header("Shadow Settings")]
    public Transform shadowObject;
    public float shadowRange = 2.0f;

    private Vector3 pillarStartPos;
    private Vector3 shadowStartPos;

    void Start()
    {
        pillarStartPos = transform.position;
        if (shadowObject != null) shadowStartPos = shadowObject.position;
        new WaitForSeconds(offset);
    }

    void Update()
    {
        // Calculate 'Slow Up, Fast Down'
        float cycleProgress = (Time.time / cycleDuration) % 1.0f;
        float movementFactor = 0f;

        if (cycleProgress < upTimePercent)
            movementFactor = Mathf.SmoothStep(0, 1, cycleProgress / upTimePercent);
        else
        {
            float downProgress = (cycleProgress - upTimePercent) / (1f - upTimePercent);
            movementFactor = 1.0f - downProgress; 
        }

        transform.position = pillarStartPos + Vector3.up * (movementFactor * height);

        if (shadowObject != null)
            shadowObject.position = shadowStartPos + (shadowObject.up * (movementFactor * shadowRange));
    }

    // --- COLLISION DETECTION ---
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object we touched is tagged as "Player"
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<Playermove>();
            if (player != null)
            {
                // player.die();
                Debug.Log("Pillar crushed the player!");
            }
        }
    }
}