using UnityEngine;

public class playerAnimationEvent : MonoBehaviour
{
    private Playermove player;
    private void Awake()
    {
        player = GetComponentInParent<Playermove>();
    }

    private void disableMovement() => player.ToggleMovement(false);

    private void enableMovement() => player.ToggleMovement(true);

    // public void DealDamage() => player.DealDamage();
    
    public void AttackEnd() => player.attackEnd();

}
