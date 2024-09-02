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

    /// <summary>
    /// Sets the HighScore to match the HighScore at the start
    /// </summary>
    private void Start()
    {
        HighScoreText.text = "HighScore: " + PlayerPrefs.GetInt("HighScore");
    }

    /// <summary>
    /// Upon collision with an enemy, reduces lives by one, plays the hurtSound, and if lives = 0 Restarts the scene
    /// </summary>
    /// <param name="collision"></param>
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

    /// <summary>
    /// Increases Score, changes ScoreText to match, checks if Score exceeds the HighScore PlayerPref, and if it does,
    /// updates HighScore and HighScoreText to match
    /// </summary>
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
