using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float health = 100f;

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Enemy took " + damage + " damage. Health: " + health);
        if (health <= 0)
        {
            Debug.Log("Died");
        }
    }
}
