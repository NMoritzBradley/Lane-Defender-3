using System.Collections;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    Rigidbody2D rB2D;
    public GameObject Snake;
    public GameObject DyingSnake;
    private int speed = -7;

    /// <summary>
    /// Gets the rigidbody
    /// </summary>
    void Start()
    {
        rB2D = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Moves it left
    /// </summary>
    private void FixedUpdate()
    {
        rB2D.velocity = new Vector2(speed, 0);
    }

    /// <summary>
    /// When hit by a bullet, stops, destroys the Snake, and creates a DyingSnake object. The Dying Snake has the 
    /// hit animation and the hit explosion. I know it's a weird way of doing it, but I didn't know how to change 
    /// animations, and it looks the same as if the Snake object did those animations. It also destroys the slime on
    /// contact with the player. The player's collision box, by the way, also makes up the back wall
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            speed = 0;
            Invoke("CreateDyingSnake", .1f);
            StartCoroutine(Die());
        }

        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Destroys the object
    /// </summary>
    /// <returns></returns>
    private IEnumerator Die()
    {
        yield return new WaitForSeconds(.2f);
        Destroy(gameObject);
    }

    /// <summary>
    /// Creates a DyingSnake over the Snake
    /// </summary>
    void CreateDyingSnake()
    {
        transform.position = Snake.GetComponent<Rigidbody2D>().position;

        Instantiate(DyingSnake, (transform.position), Quaternion.identity);
    }
}
