using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;

    private float spawnRate = 1.0f;

    private float score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI liveText;

    public Button restartButton;
    public GameObject titleScreen;

    public GameObject pauseScreen;
    public bool paused;

    public bool isGameActive;
    public int live;

    private void Awake()
    {

    }

    void Start()
    {
        
    }

    void Update()
    {
        //Check if the user has pressed the P key
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangePaused();
        }
    }


    public void StartGame (int difficulty)
    {
        isGameActive = true;
        score = 0;
        live = 3;
        spawnRate /= difficulty;

        titleScreen.gameObject.SetActive(false);
        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        Live(0);
    }

    void ChangePaused()
    {
        if (!paused)
        {
            paused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            paused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void GameOver ()
    {
        isGameActive = false;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    public void RestartGame ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // Prototype 5
    }

    IEnumerator SpawnTarget ()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void UpdateScore (int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void Live (int minusLive)
    {
        live -= minusLive;
        liveText.text = "Live " + live;
    }
}
