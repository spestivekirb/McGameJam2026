using UnityEngine;
using UnityEngine.Rendering.Universal; 

public class Lighthouse2D : MonoBehaviour
{
    [Header("Components")]
    public Light2D lightSource;

    [Header("Intensity Pulse")]
    public float minIntensity = 0.5f;
    public float maxIntensity = 2.5f;
    public float pulseSpeed = 3.0f;

    [Header("Movement")]
    public float rotationSpeed = 100f;

    void Update()
    {
        if (lightSource == null) return;

        // Calculate smooth pulse using Sine
        // Maps -1 to 1 range into a 0 to 1 range
        float wave = (Mathf.Sin(Time.time * pulseSpeed) + 1f) / 2f;
        
        // Apply intensity
        lightSource.intensity = Mathf.Lerp(minIntensity, maxIntensity, wave);

        // Optional: Rotate the light object
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}