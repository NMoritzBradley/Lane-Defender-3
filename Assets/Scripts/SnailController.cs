using System.Collections;
using UnityEngine;

public class SnailController : MonoBehaviour
{
    Rigidbody2D rB2D;
    public GameObject Snail;
    public GameObject DyingSnail;
    private int speed = -3;
    private int lives = 5;

    /// <summary>
    /// Gets the rigidbody
    /// </summary>
    void Start()
    {
        rB2D = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Makes it move left
    /// </summary>
    private void FixedUpdate()
    {
        rB2D.velocity = new Vector2(speed, 0);
    }

    /// <summary>
    /// When hit by a bullet, stops, loses one life, and creates a DyingSnail object. The Dying Snail has the 
    /// hit animation and the hit explosion. I know it's a weird way of doing it, but I didn't know how to change 
    /// animations, and it looks the same as if the Snail object did those animations. It also destroys the snail on
    /// contact with the player. The player's collision box, by the way, also makes up the back wall
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            speed = 0;
            lives -= 1;
            Invoke("CreateDyingSnail", .1f);
            if (lives > 0)
            {
                StartCoroutine(Recover());
            }
            else if (lives == 0)
            {
                StartCoroutine(Die());
            }
        }

        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Makes it start moving again after being hit
    /// </summary>
    /// <returns></returns>
    private IEnumerator Recover()
    {
        yield return new WaitForSeconds(.4f);
        speed = -3;
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
    void CreateDyingSnail()
    {
        transform.position = Snail.GetComponent<Rigidbody2D>().position;

        Instantiate(DyingSnail, (transform.position), Quaternion.identity);
    }
}
