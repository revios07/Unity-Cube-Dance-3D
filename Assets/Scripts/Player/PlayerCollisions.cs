using UnityEngine;

public class PlayerCollisions : PlayerProps
{
    private void OnCollisionEnter(Collision collision)
    {
        //Player Dead
        if (collision.gameObject.CompareTag("DeadArea") && !_isGameOver)
        {
            _isGameOver = true;

            _uiController.ScoreToEndGame(score);

            //Reload High Score
            if (score > PlayerPrefs.GetFloat("HighScore", 0.0f))
            {
                PlayerPrefs.SetFloat("HighScore", score);
                _uiController.HighScoreAtEndGameText(score);
            }
            else
            {
                _uiController.HighScoreAtEndGameText(PlayerPrefs.GetFloat("HighScore", 0));
            }

            _uiController.GameOverScreen();
        }
        else
        {
            Debug.Log("Player Still Dead");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Touch To Score Multiplier Obj
        if (other.gameObject.CompareTag("ScoreMultiplier"))
        {
            //Player At Mid Of Hole In Point
            if (transform.position.y - other.transform.position.z < 0.25f)
            {
                score += 12f;
            }
            else if (transform.position.y - other.transform.position.z < 0.5f)
            {
                score += 8f;
            }
            else
            {
                score += 4f;
            }
        }

        other.gameObject.SetActive(false);
    }
}
