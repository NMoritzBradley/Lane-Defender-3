using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    Rigidbody2D rB2D;
    public GameObject Snake;
    public GameObject DyingSnake;
    private int speed = -7;

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
            Invoke("CreateDyingSnake", .1f);
            StartCoroutine(Die());
        }

        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(.2f);
        Destroy(gameObject);
    }

    void CreateDyingSnake()
    {
        transform.position = Snake.GetComponent<Rigidbody2D>().position;

        Instantiate(DyingSnake, (transform.position), Quaternion.identity);
    }
}
