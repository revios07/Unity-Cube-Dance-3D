using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreTextEndGame;
    [SerializeField]
    private Text _highscoreText;
    [SerializeField]
    private GameObject _gameOverGameObj;
    [SerializeField]
    public Text realtimeScoreText;
    [SerializeField]
    public GameObject holdStartText;

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void HighScoreAtEndGameText(float highScore)
    {
        _highscoreText.text = "HighestScore: " + highScore.ToString("0.0");
    }

    public void ScoreToEndGame(float score)
    {
        _scoreTextEndGame.text = score.ToString("0.0");
    }

    //Open Game Over Screen
    public void GameOverScreen()
    {
        Invoke(nameof(WaitForSkipLoseScreen),1f);

        _gameOverGameObj.SetActive(true);
        realtimeScoreText.enabled = false;
    }

    private void WaitForSkipLoseScreen()
    {
        holdStartText.SetActive(true);
    }
}
