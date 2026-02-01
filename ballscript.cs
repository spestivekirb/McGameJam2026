using UnityEngine;

public class ballscript : MonoBehaviour
{
    public float strikeForce = 1f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 pushDir = transform.position - collision.transform.position;
            rb.AddForce(pushDir.normalized * strikeForce, ForceMode2D.Impulse);
        }
    }
}