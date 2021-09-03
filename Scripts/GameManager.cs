using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool gameStarted = false;
    public GameObject player;
   public Text scoreText;
    public Text liveText;
    [SerializeField] GameObject effect;


    [SerializeField] GameObject menuUi;
    [SerializeField] GameObject playUI;
    [SerializeField] GameObject spawner;

    int lives = 3;
    int score = 0;

    Vector3 origCamerapos;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        origCamerapos = Camera.main.transform.position;
    }

    public void StartGame()
    {
        gameStarted = true;
        menuUi.SetActive(false);
        playUI.SetActive(true);
        spawner.SetActive(true);
        effect.SetActive(true);
    }
    public void GameOver()
    {
        player.SetActive(false);
        Invoke(nameof(RelodLevel), 2f);
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
            Vector2 ranpos = Random.insideUnitCircle * 0.5f;
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
        score++;
        scoreText.text = score.ToString();
    }
    public void ExGame()
    {
        Application.Quit();
    }
   
}
