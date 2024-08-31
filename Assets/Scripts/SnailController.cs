using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailController : MonoBehaviour
{
    Rigidbody2D rB2D;
    public GameObject Snail;
    public GameObject DyingSnail;
    private int speed = -3;
    private int lives = 5;

    // Start is called before the first frame update
    void Start()
    {
        rB2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rB2D.velocity = new Vector2(speed, 0);
    }

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

    private IEnumerator Recover()
    {
        yield return new WaitForSeconds(.4f);
        speed = -3;
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(.2f);
        Destroy(gameObject);
    }

    void CreateDyingSnail()
    {
        transform.position = Snail.GetComponent<Rigidbody2D>().position;

        Instantiate(DyingSnail, (transform.position), Quaternion.identity);
    }
}
