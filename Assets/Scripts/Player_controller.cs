using UnityEngine;

public class Player_controller : MonoBehaviour
{
    public Rigidbody2D rb;
    public float Speed = 1.0f;
    public GameObject Bullet;
    public float Bullet_speed = 1.0f;
    public AudioClip Bullet_shoot;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.rotation = Quaternion.identity;
        transform.position = new Vector2(0, -4);
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.identity;

        if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x >= -8.1f)
            rb.linearVelocity = new Vector2(-Speed, rb.linearVelocityY);

        else if (Input.GetKey(KeyCode.RightArrow) && transform.position.x <= 8.1f)
            rb.linearVelocity = new Vector2(Speed, rb.linearVelocityY);

        else
            rb.linearVelocity = Vector2.zero;


        if (Input.GetKeyDown(KeyCode.Space))
        {
            AudioSource.PlayClipAtPoint(Bullet_shoot, transform.position);
            Instantiate(Bullet, transform.position, Quaternion.identity);
        }
    }
}