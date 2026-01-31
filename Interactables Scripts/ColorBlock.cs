using UnityEngine;

public class ColorBlock : MonoBehaviour
{

    public bool isRed;

    public Collider2D boxcollider;
    public Color oncolor;
    private Color offcolor;

    private SpriteRenderer spriterender;

    void Awake()
    {
        boxcollider = GetComponent<Collider2D>();
        spriterender = GetComponent<SpriteRenderer>();
        
        oncolor = new Color(spriterender.color.r, spriterender.color.g, spriterender.color.b, 1.0f);
        offcolor = new Color(oncolor.r, oncolor.g, oncolor.b, 0.3f);
    }

    public void UpdateState(bool redOn)
    {
        bool isOn = isRed == redOn;

        boxcollider.enabled = isOn;

        if (isOn)
        {
            spriterender.color = oncolor;
        } else
        {
            spriterender.color = offcolor;
        }
    }
}
