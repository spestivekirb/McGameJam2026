using UnityEngine;

public class shadowAnimationEvent : MonoBehaviour
{
    private shadowFollower shadow;
    private void Awake()
    {
        shadow = GetComponentInParent<shadowFollower>();
    }

}
