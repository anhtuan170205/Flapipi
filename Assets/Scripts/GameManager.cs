using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{   
    public static GameManager Instance;
    public event Action OnGameStarted;
    public event Action OnGameOver;
    public event Action OnGameRestarted;
    
    private bool isGameStarted = false;
    private bool isGameOver = false;
    private bool canRestart = false; 

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

    private void Start()
    {
        Player.OnPlayerDied += Player_OnPlayerDied;
    }

    public void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && !isGameStarted && !isGameOver)
        {
            isGameStarted = true;
            OnGameStarted?.Invoke();
        }
        else if (isGameOver && canRestart && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))) 
        {
            RestartGame();
        }
    }

    private void Player_OnPlayerDied()
    {
        isGameOver = true;
        canRestart = false; 
        OnGameOver?.Invoke();

        StartCoroutine(EnableRestartAfterDelay()); 
    }

    private IEnumerator EnableRestartAfterDelay()
    {
        yield return new WaitForSeconds(1f); 
        canRestart = true;
    }

    private void RestartGame()
    {
        isGameStarted = false;
        isGameOver = false;
        canRestart = false; 
        OnGameRestarted?.Invoke();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
