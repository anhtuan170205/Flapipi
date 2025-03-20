using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
    public static UIDisplay Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject menuTextGroup;
    [SerializeField] private GameObject gameOverTextGroup;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI currentScoreText;
    private int currentScore = 0;

    private void Start()
    {
        ScoreManager.Instance.OnScoreChanged += ScoreManager_OnScoreChanged;
        GameManager.Instance.OnGameStarted += GameManager_OnGameStarted;
        GameManager.Instance.OnGameOver += GameManager_OnGameOver;
        GameManager.Instance.OnGameRestarted += GameManager_OnGameRestarted;
        scoreText.gameObject.SetActive(false);
        menuTextGroup.SetActive(true);
        gameOverTextGroup.SetActive(false);
    }
    private void ScoreManager_OnScoreChanged()
    {
        scoreText.text = ScoreManager.Instance.GetScore().ToString("00");
        currentScore++;
    }
    private void GameManager_OnGameStarted()
    {
        scoreText.gameObject.SetActive(true);
        menuTextGroup.SetActive(false);
    }
    private void GameManager_OnGameOver()
    {
        scoreText.gameObject.SetActive(false);
        gameOverTextGroup.SetActive(true);
        highScoreText.text = "BEST: " + ScoreManager.Instance.GetHighScore().ToString("00");
        currentScoreText.text = "SCORE: " + (currentScore - 1).ToString("00");
        currentScore = 0;
    }
    private void GameManager_OnGameRestarted()
    {
        scoreText.gameObject.SetActive(true);
        menuTextGroup.SetActive(false);
        gameOverTextGroup.SetActive(false);
    }
    private void OnDestroy()
    {
        ScoreManager.Instance.OnScoreChanged -= ScoreManager_OnScoreChanged;
        GameManager.Instance.OnGameStarted -= GameManager_OnGameStarted;
        GameManager.Instance.OnGameOver -= GameManager_OnGameOver;
    }
}
