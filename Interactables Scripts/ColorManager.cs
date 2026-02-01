using UnityEngine;
using System.Collections;

public class ColorManager : MonoBehaviour, IActivatable
{
    public bool redOn = true;
    public ColorBlock[] blocks;
    public ShadowCollider[] shadowblocks;
    public shadowFollower shadow_script;

    void Awake()
    {
 
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (ColorBlock block in blocks)
        {
            block.UpdateState(redOn);
        }

        foreach (ShadowCollider shadowblock in shadowblocks)
        {
            shadowblock.UpdateState(redOn);
        }
    }
    public void Activate()
    {
        redOn = !redOn;
        foreach (ColorBlock block in blocks)
        {
            block.UpdateState(redOn);
        }
        StartCoroutine(ShadowUpdate(redOn, shadow_script.GetDelay()));
    }
    private IEnumerator ShadowUpdate(bool newState, float delay)
    {
        yield return new WaitForSeconds(delay);
        foreach (ShadowCollider shadowblock in shadowblocks)
        {
            shadowblock.UpdateState(newState);
        }
    }

    public void Deactivate()
    {
        // No behaviour
    }

}
