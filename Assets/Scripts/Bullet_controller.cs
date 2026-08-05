using UnityEngine;

public class Bullet_controller : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;

        if (transform.position.y >= 5.5f)
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            FindFirstObjectByType<Game_manager>().Score_Count();
        }
    }
}