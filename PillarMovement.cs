using UnityEngine;

public class PillarMover : MonoBehaviour
{
    [Header("Pillar Settings")]
    public float cycleDuration = 3.0f; 
    public float height = 5.0f;
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
        if (shadowObject != null)
        {
            // We store the World position to calculate relative movement
            shadowStartPos = shadowObject.position;
        }
    }

    void Update()
    {
        // 1. Calculate the 'Slow Up, Fast Down' factor
        float cycleProgress = (Time.time / cycleDuration) % 1.0f;
        float movementFactor = 0f;

        if (cycleProgress < upTimePercent)
        {
            movementFactor = Mathf.SmoothStep(0, 1, cycleProgress / upTimePercent);
        }
        else
        {
            float downProgress = (cycleProgress - upTimePercent) / (1f - upTimePercent);
            movementFactor = 1.0f - downProgress; 
        }

        // 2. Move Pillar (Always World Up)
        transform.position = pillarStartPos + Vector3.up * (movementFactor * height);

        // 3. Move Shadow (Along its own Red Arrow)
        if (shadowObject != null)
        {
            // Move from start position along the shadow's 'Right' vector
            shadowObject.position = shadowStartPos + (shadowObject.up * (movementFactor * shadowRange));
        }
    }
}