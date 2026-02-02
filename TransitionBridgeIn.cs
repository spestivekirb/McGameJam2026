using UnityEngine;
using System.Collections;
using System.Collections.Generic; // Required for Dictionary

public class TransitionBridgeIn : MonoBehaviour
{
    public GameObject shadow;

    [Header("Movement Settings")]
    public float riseAmount = 20f;
    public float moveSpeed = 4f;
    public float staggerDelay = 1f;

    [Header("Timed Bridge Settings")]
    public bool isTemporary = false;
    public float stayTime = 5f;   

    // MEMORY SYSTEM: Stores the exact start and end points for every block
    private Dictionary<Transform, Vector3> homePositions = new Dictionary<Transform, Vector3>();
    private Dictionary<Transform, Vector3> hiddenPositions = new Dictionary<Transform, Vector3>();
    
    private bool hasTriggered = false;

    void Awake()
    {
        foreach (Transform child in transform)
        {
    
            homePositions[child] = child.localPosition; 
            
            Vector3 hidden = child.localPosition - new Vector3(0, riseAmount, 0);
            hiddenPositions[child] = hidden;

            child.localPosition = hidden;
        }
    }

    [ContextMenu("Run Intro Animation")]
    public void PlayIntro()
    {
        if (hasTriggered) return; 
        
        Debug.Log("Running bridge sequence");
        StartCoroutine(SequenceRoutine());
    }
  
    IEnumerator SequenceRoutine()
    {
        hasTriggered = true;

        if (shadow != null) shadow.SetActive(false);

        foreach (Transform child in transform)
        {
            StartCoroutine(MoveToTarget(child, homePositions[child])); 
            yield return new WaitForSeconds(staggerDelay);
        }

        if (isTemporary)
        {
            yield return new WaitForSeconds(stayTime);


            foreach (Transform child in transform)
            {
                StartCoroutine(MoveToTarget(child, hiddenPositions[child])); 
                yield return new WaitForSeconds(staggerDelay);
            }
            
            hasTriggered = false;

        }
    }

    IEnumerator MoveToTarget(Transform item, Vector3 targetPos)
    {
        Vector3 startPos = item.localPosition;
        float t = 0;
        
        while (t < 1f)
        {
            t += Time.deltaTime * moveSpeed;
            item.localPosition = Vector3.Lerp(startPos, targetPos, Mathf.SmoothStep(0, 1, t));
            yield return null;
        }
        item.localPosition = targetPos; 
    }
}
