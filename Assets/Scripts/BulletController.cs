using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BulletController : MonoBehaviour
{
    Rigidbody2D rB2D;
    public LivesTracker LT;

    // Start is called before the first frame update
    void Start()
    {
        rB2D = GetComponent<Rigidbody2D>();
        LT = FindObjectOfType<LivesTracker>();
    }

    private void FixedUpdate()
    {
        rB2D.velocity = new Vector2(10, 0);
    }

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
