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
    public GameObject deadBird;

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
        Time.timeScale = 0f;
        player.enabled = false;
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
        if (score == 10)
        {
            spawner.GetComponent<Spawner>().DiffRamp();
        }

    }

}
