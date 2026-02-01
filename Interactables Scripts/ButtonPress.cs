using UnityEngine;

public class ButtonPress : MonoBehaviour
{
    public float pressdepth = 0.20f;
    public Vector3 startpos;
    public Vector3 presspos;

    public GameObject targetObject;

    public Transform visualSprite;
    public TransitionScript bridgeScript; // for lvl 5 bridge animation
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startpos = visualSprite.localPosition;
        presspos = startpos - new Vector3(0, pressdepth, 0);;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void animateTransitionLevel5(){
        // Visually press the button down
        visualSprite.localPosition = presspos;

        // Trigger the bridge if the reference exists
        if (bridgeScript != null)
        {
            bridgeScript.StartBridgeRise();
        }
        else 
        {
            Debug.LogError("BridgeSequencer reference is missing on the Button!");
        }
    }

    void OnTriggerEnter2D(Collider2D other) {

        if (other.CompareTag("Player") || other.CompareTag("Shadow")|| other.CompareTag("Ball")|| other.CompareTag("Bucket"))
        {   
            if (other.CompareTag("Bucket")){
                animateTransitionLevel5();
            }
            else {
                visualSprite.localPosition = presspos;
                Debug.LogWarning("Button Pressed");
            }
        }
        IActivatable action = targetObject.GetComponent<IActivatable>();

        if (action != null)
        {
            action.Activate();
        } else
        {
            Debug.LogWarning("No Action");
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        
        if (other.CompareTag("Player") || other.CompareTag("Shadow")|| other.CompareTag("Ball"))
        {


            visualSprite.localPosition = startpos;

        }
        IActivatable action = targetObject.GetComponent<IActivatable>();

        if (action != null)
        {
            action.Deactivate();
        } else
        {
            Debug.LogWarning("No Action");
        }
    }
}
