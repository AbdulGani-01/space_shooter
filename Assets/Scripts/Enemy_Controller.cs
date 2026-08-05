using UnityEngine;

public class Enemy_Controller : MonoBehaviour
{
    public float speed = 1f;
    private Rigidbody2D rb;

    private void Start()
    {
        FindAnyObjectByType<Game_manager>();
    }
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.linearVelocity = Vector2.down * speed;

        if (FindFirstObjectByType<Game_manager>().score >= 10)
            speed = 1.5f;

        else if (FindFirstObjectByType<Game_manager>().score >= 20)
            speed = 2f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
            Destroy(gameObject);

        if (collision.gameObject.CompareTag("Border"))
        {
            FindFirstObjectByType<Game_manager>().GameOver();
        }
    }
}