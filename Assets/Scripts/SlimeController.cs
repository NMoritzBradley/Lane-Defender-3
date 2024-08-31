using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    Rigidbody2D rB2D;
    public GameObject Slime;
    public GameObject DyingSlime;
    private int speed = -5;
    private int lives = 3;

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
            Invoke("CreateDyingSlime", .1f);
            if(lives > 0)
            {
                StartCoroutine(Recover());
            }
            else
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
        speed = -5;
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(.2f);
        Destroy(gameObject);
    }

    void CreateDyingSlime()
    {
        transform.position = Slime.GetComponent<Rigidbody2D>().position;

        Instantiate(DyingSlime, (transform.position), Quaternion.identity);
    }
}
