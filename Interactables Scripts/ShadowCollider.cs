using UnityEngine;

public class ShadowCollider : MonoBehaviour
{
    public bool isRed;

    public Collider2D boxcollider;


    void Awake()
    {
        boxcollider = GetComponent<Collider2D>();
    }

    public void UpdateState(bool redOn)
    {
        bool isOn = isRed == redOn;

        boxcollider.enabled = isOn;
        
    }

}