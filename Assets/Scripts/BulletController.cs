using UnityEngine;

public class BulletController : MonoBehaviour
{
    Rigidbody2D rB2D;
    public LivesTracker LT;

    /// <summary>
    /// Finds the Rigidbody and the LivesTracker
    /// </summary>
    void Start()
    {
        rB2D = GetComponent<Rigidbody2D>();
        LT = FindObjectOfType<LivesTracker>();
    }

    /// <summary>
    /// Moves the bullet right
    /// </summary>
    private void FixedUpdate()
    {
        rB2D.velocity = new Vector2(10, 0);
    }

    /// <summary>
    /// Destroys the bullet when it hits a wall or an enemy, and calls UpdateScore in LivesTracker when it hits an enemy
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "Enemy")
        {
            LT.UpdateScore();
            Destroy(gameObject);
        }
    }
}
