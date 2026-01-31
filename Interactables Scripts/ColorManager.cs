using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour, IActivatable
{
    private bool redOn = true;
    public ColorBlock[] blocks;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        blocks = FindObjectsByType<ColorBlock>(FindObjectsSortMode.None);
        foreach (ColorBlock block in blocks)
        {
            block.UpdateState(redOn);
        }
    }
    public void Activate()
    {
        redOn = !redOn;
        foreach (ColorBlock block in blocks)
        {
            block.UpdateState(redOn);
        }
    }

    public void Deactivate()
    {
        // No behaviour
    }

}
