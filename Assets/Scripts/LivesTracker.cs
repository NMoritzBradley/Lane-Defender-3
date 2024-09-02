using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LivesTracker : MonoBehaviour
{
    public TMP_Text ScoreText;
    public TMP_Text HighScoreText;
    public AudioClip hurtSound;
    private int lives = 3;
    public int Score = 0;
    public int ScoreIncrease = 1000;

    private void Start()
    {
        HighScoreText.text = "HighScore: " + PlayerPrefs.GetInt("HighScore");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            lives -= 1;
            AudioSource.PlayClipAtPoint(hurtSound, transform.position);
            if (lives == 0)
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    public void UpdateScore()
    {
        Score += ScoreIncrease;
        ScoreText.text = "Score: " + Score; 
        if(Score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", Score);
        }
        HighScoreText.text = "HighScore: " + PlayerPrefs.GetInt("HighScore");
    }
}
