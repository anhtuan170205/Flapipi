using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
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
    private int score;
    private int highScore;
    public event Action OnScoreChanged;

    private void Start()
    {
        score = 0;
        Player.OnPlayerScored += Player_OnPlayerScored;
        GameManager.Instance.OnGameOver += GameManager_OnGameOver;
    }
    private void AddScore()
    {
        score++;
        if (score > highScore)
        {
            highScore = score;
        }
        OnScoreChanged?.Invoke();
    }

    private void Player_OnPlayerScored()
    {
        AddScore();
    }

    private void GameManager_OnGameOver()
    {
        ResetScore();
    }

    public int GetScore()
    {
        return score;
    }
    private void ResetScore()
    {
        score = 0;
        OnScoreChanged?.Invoke();
    }
    public int GetHighScore()
    {
        return highScore;
    }
    private void OnDestroy()
    {
        Player.OnPlayerScored -= Player_OnPlayerScored;
        GameManager.Instance.OnGameOver -= GameManager_OnGameOver;
    }
}
