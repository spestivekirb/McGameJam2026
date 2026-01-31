using UnityEngine;

public class ButtonPress : MonoBehaviour
{
    public float pressdepth = 0.20f;
    public Vector3 startpos;
    public Vector3 presspos;

    public GameObject targetObject;

    public Transform visualSprite;

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


    void OnTriggerEnter2D(Collider2D other) {

        if (other.CompareTag("Player"))
        {   


            visualSprite.localPosition = presspos;
            
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
        
        if (other.CompareTag("Player"))
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
