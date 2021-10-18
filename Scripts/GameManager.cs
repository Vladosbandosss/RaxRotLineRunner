using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Text scoreTextfin;
    [SerializeField] Text highScoreTextfin;


    [SerializeField] GameObject goPanel;
    public static GameManager instance;
    public bool gameStarted = false;
    public GameObject player;
   public Text scoreText;
    public Text liveText;
    [SerializeField] GameObject effect;


    [SerializeField] GameObject menuUi;
    [SerializeField] GameObject playUI;
    [SerializeField] GameObject spawner;

    AudioSource bg;
    int lives = 3;
    int score = 0;

    Vector3 origCamerapos;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        bg = GetComponent<AudioSource>();
    }
    private void Start()
    {
        origCamerapos = Camera.main.transform.position;
        
    }

    public void StartGame()
    {
        goPanel.SetActive(false);
        gameStarted = true;
        menuUi.SetActive(false);
        playUI.SetActive(true);
        spawner.SetActive(true);
        effect.SetActive(true);
        bg.Play();

    }
    public void GameOver()
    {
        ShowFinelRes();
        goPanel.SetActive(true);
        gameStarted = false;
        bg.Stop();
        Invoke(nameof(DeactivePlayer), 1f);
        Invoke(nameof(RelodLevel), 4f);
        effect.SetActive(false);


    }
    public void RelodLevel()
    {
        SceneManager.LoadScene("Game");
    }
    public void UpdateLives()
    {
        if (lives <= 0)
        {
            GameOver();
        }
        else
        {
            lives--;
            liveText.text = "Lives: " + lives.ToString();
            if (lives <= 0)
            {
                GameOver();
            }


        }
    }

    IEnumerator ShakeCamera()
    {
        for (int i = 0; i < 5; i++)
        {
            Vector2 ranpos = Random.insideUnitCircle * 1f;
            Camera.main.transform.position = new Vector3(ranpos.x, ranpos.y,Camera.main.transform.position.z);
            yield return null;
        }
        Camera.main.transform.position = origCamerapos;
    }

    public void Shake()
    {
        StartCoroutine(nameof(ShakeCamera));
    }

    public void UpdateScore()
    {
        if (gameStarted)
        {
            score++;
            scoreText.text = score.ToString();
        }
       
    }
    public void ExGame()
    {
        Application.Quit();
    }

    void DeactivePlayer()
    {
        player.SetActive(false);
    }

    public void ShowFinelRes()
    {
        if (PlayerPrefs.HasKey("highScore"))
        {
            if (score > PlayerPrefs.GetInt("highScore"))
            {
                PlayerPrefs.SetInt("highScore", score);
                highScoreTextfin.text = score.ToString();
            }
            else
            {
                highScoreTextfin.text = PlayerPrefs.GetInt("highScore").ToString();
            }

        }
        else
        {
            PlayerPrefs.SetInt("highScore", score);
            highScoreTextfin.text = score.ToString();
        }

        scoreTextfin.text = score.ToString();
    }
   
}
