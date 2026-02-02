using UnityEngine;
using System.Collections;

public class TransitionScript : MonoBehaviour
{
    public float moveDistance = 20f;
    public float moveSpeed = 5f;
    public float delayBetweenItems = 0.2f;
    public TransitionBridgeIn bridgeInScript; // for lvl 5 bridge animation

    [ContextMenu("Trigger Rise")] // Allows you to test by right-clicking the component
    public void StartBridgeRise()
    {
        StartCoroutine(RiseSequence());
    }

    IEnumerator RiseSequence()
    {
        // Loop through every child of the TransitionSet
        foreach (Transform child in transform)
        {
            StartCoroutine(MoveItem(child));
            
            // Wait before starting the next bridge piece
            yield return new WaitForSeconds(delayBetweenItems);
        }
        // // now call bridge rise
        bridgeInScript.PlayIntro();
    }

    IEnumerator MoveItem(Transform item)
    {
        Vector3 startPos = item.localPosition;
        Vector3 targetPos = startPos - new Vector3(0, moveDistance, 0);
        float elapsed = 0;
        float duration = 1f / moveSpeed;

        while (elapsed < duration)
        {
            item.localPosition = Vector3.Lerp(startPos, targetPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        item.localPosition = targetPos;
    }
}