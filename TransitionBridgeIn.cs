using UnityEngine;
using System.Collections;

public class TransitionBridgeIn : MonoBehaviour
{
    [Header("Movement Settings")]
    public float riseAmount = 20f;
    public float moveSpeed = 4f;
    public float staggerDelay = 0.15f;

    [ContextMenu("Run Intro Animation")]
    public void PlayIntro()
    {
        Debug.Log("ok now running bridge in");
        StartCoroutine(AnimatePiecesIn());
    }

    void Awake()
    {
        // Option A: Move them down 20 units instantly so they are ready to rise
        foreach (Transform child in transform)
        {
            child.localPosition -= new Vector3(0, riseAmount, 0);
        }

        // Option B: If you want them totally invisible/inactive until the button is hit
        // gameObject.SetActive(false); 
    }

    IEnumerator AnimatePiecesIn()
    {
        // Loop through all 20+ bridge pieces
        foreach (Transform child in transform)
        {
            StartCoroutine(SmoothMove(child));
            // Wait a fraction of a second before starting the next one
            yield return new WaitForSeconds(staggerDelay);
        }
    }

    IEnumerator SmoothMove(Transform item)
    {
        Vector3 startPos = item.localPosition;
        Vector3 endPos = startPos + new Vector3(0, riseAmount, 0);
        float t = 0;

        while (t < 1f)
        {
            t += Time.deltaTime * moveSpeed;
            // Use SmoothStep for a "heavy" mechanical feel
            item.localPosition = Vector3.Lerp(startPos, endPos, Mathf.SmoothStep(0, 1, t));
            yield return null;
        }

        item.localPosition = endPos;
    }
}