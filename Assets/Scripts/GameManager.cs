using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public bool gameActive = false;
    private Player player;
    private Spawner spawner;
    public Text scoreText;
    public GameObject playButton;
    public GameObject gameOver;
    public int scoreIndex = 0;
    public GameObject icon;
    public int score { get; private set; }
    public GameObject getReady;
    public int highScore { get; private set; }
    public TMP_Text highScoreText;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        player = FindObjectOfType<Player>();
        spawner = FindObjectOfType<Spawner>();

        Pause();
    }

    public void Play()
    {
        gameActive = true;
        score = 0;
        scoreText.text = score.ToString();


        getReady.SetActive(false);
        playButton.SetActive(false);
        gameOver.SetActive(false);
        icon.SetActive(false);

        Time.timeScale = 1f;
        player.enabled = true;

        Pipes[] pipes = FindObjectsOfType<Pipes>();
        spawner.spawnRate = 1f;
        spawner.gameObject.SetActive(true);

        for (int i = 0; i < pipes.Length; i++)
        {
            Destroy(pipes[i].gameObject);
        }
    }

    public void GameOver()
    {
        spawner.gameObject.SetActive(false);

        gameActive = false;
        playButton.SetActive(true);
        gameOver.SetActive(true);
        icon.SetActive(true);
        Pause();
    }

    public void Pause()
    {
        player.enabled = false;
        Time.timeScale = 0f;
    }

    public void IncreaseScore()
    {
        score++;
        if (highScore < score) {
            highScore = score;
            highScoreText.text = highScore.ToString();
        }
        scoreText.text = score.ToString();
        if (score == 10)
        {
            spawner.GetComponent<Spawner>().DiffRamp();
        }

    }

}
