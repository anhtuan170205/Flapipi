using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private AudioClip scoreSound;
    [SerializeField] private AudioClip gameOverSound;
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
        GameManager.Instance.OnGameStarted += GameManager_OnGameStarted;
        GameManager.Instance.OnGameOver += GameManager_OnGameOver;
        GameManager.Instance.OnGameRestarted += GameManager_OnGameRestarted;
        Player.OnPlayerDied += Player_OnPlayerDied;
    }
    private void GameManager_OnGameStarted()
    {
        AudioSource.PlayClipAtPoint(backgroundMusic, Camera.main.transform.position);
    }
    private void GameManager_OnGameOver()
    {
        AudioSource.PlayClipAtPoint(gameOverSound, Camera.main.transform.position);
    }
    private void GameManager_OnGameRestarted()
    {
        AudioSource.PlayClipAtPoint(gameOverSound, Camera.main.transform.position);
    }
    private void Player_OnPlayerDied()
    {
        AudioSource.PlayClipAtPoint(scoreSound, Camera.main.transform.position);
    }
    
    private void OnDestroy()
    {
        GameManager.Instance.OnGameStarted -= GameManager_OnGameStarted;
        GameManager.Instance.OnGameOver -= GameManager_OnGameOver;
        GameManager.Instance.OnGameRestarted -= GameManager_OnGameRestarted;
        Player.OnPlayerDied -= Player_OnPlayerDied;
    }
}
