using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LivesTracker : MonoBehaviour
{
    private int lives = 3;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            lives -= 1;
            print(lives);
            if (lives == 0)
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}
