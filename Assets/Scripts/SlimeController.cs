using System.Collections;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    Rigidbody2D rB2D;
    public GameObject Slime;
    public GameObject DyingSlime;
    private int speed = -5;
    private int lives = 3;

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
    /// When hit by a bullet, stops, loses one life, and creates a DyingSlime object. The Dying Slime has the 
    /// hit animation and the hit explosion. I know it's a weird way of doing it, but I didn't know how to change 
    /// animations, and it looks the same as if the Slime object did those animations. It also destroys the slime on
    /// contact with the player. The player's collision box, by the way, also makes up the back wall
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            speed = 0;
            lives -= 1;
            Invoke("CreateDyingSlime", .1f);
            if(lives > 0)
            {
                StartCoroutine(Recover());
            }
            else if(lives == 0)
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
        speed = -5;
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
    /// Creates a DyingSlime over the Slime
    /// </summary>
    void CreateDyingSlime()
    {
        transform.position = Slime.GetComponent<Rigidbody2D>().position;

        Instantiate(DyingSlime, (transform.position), Quaternion.identity);
    }
}
