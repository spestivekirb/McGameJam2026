using UnityEngine;

public class ColorBlock : MonoBehaviour
{

    public bool isRed;

    public Collider2D boxcollider;

    private SpriteRenderer spriterender;

    void Awake()
    {
        boxcollider = GetComponent<Collider2D>();
        spriterender = GetComponent<SpriteRenderer>();
        ;
    }

    public void UpdateState(bool redOn)
    {
        bool isOn = isRed == redOn;

        boxcollider.enabled = isOn;

        Color tempColor = spriterender.color;
        
        if (isOn)
        {
            tempColor.a = 1.0f;
        }
        else
        {
            tempColor.a = 0.3f;
        }

        spriterender.color = tempColor;
    }
}
