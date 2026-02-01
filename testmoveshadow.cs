using UnityEngine;

public class testmoveshadow : MonoBehaviour
{
    [Header("Pillar Settings")]
    public float cycleDuration = 3.0f; 
    public float height = 5.0f;
    [Range(0, 1)]
    public float upTimePercent = 0.95f; // 85% up, 15% down

    [Header("Shadow Settings")]
    public Transform shadowObject;
    public float shadowRange = 2.0f;

    private Vector3 pillarStartPos;
    private Vector3 shadowWorldStartPos;

    void Start()
    {
        pillarStartPos = transform.position;
        if (shadowObject != null)
        {
            // Store the initial world position to move relative to its starting point
            shadowWorldStartPos = shadowObject.position;
        }
    }
    // Update is called once per frame
    void Update()
    {
         // 1. Calculate the movement factor (0 to 1)
        float cycleProgress = (Time.time / cycleDuration) % 1.0f;
        float movementFactor = 0f;

        if (cycleProgress < upTimePercent)
        {
            // Rise phase
            movementFactor = Mathf.SmoothStep(0, 1, cycleProgress / upTimePercent);
        }
        else
        {
            // Fall phase
            float downProgress = (cycleProgress - upTimePercent) / (1f - upTimePercent);
            movementFactor = 1.0f - downProgress; 
        }
        {
            // move shadow up (world Y)
            shadowObject.position = shadowWorldStartPos + Vector3.up * (movementFactor * shadowRange);
        }
    }
}
