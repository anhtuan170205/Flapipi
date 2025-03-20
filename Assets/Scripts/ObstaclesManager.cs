using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesManager : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private float spawnRate = 2f;

    private float nextSpawn = 0f;
    private bool isPlayerDied = false;
    private bool isGameStarted = false;
    private List<GameObject> activeObstacles = new List<GameObject>(); 

    private void Start()
    {
        Player.OnPlayerDied += Player_OnPlayerDied;
        GameManager.Instance.OnGameStarted += GameManager_OnGameStarted;
        GameManager.Instance.OnGameOver += GameManager_OnGameOver;
    }

    private void Update()
    {
        if (isPlayerDied) return;
        if (!isGameStarted) return;

        if (Time.time >= nextSpawn)
        {
            SpawnObstacle();
            nextSpawn = Time.time + spawnRate;
        }
    }

    private void SpawnObstacle()
    {
        float randomY = Random.Range(-1, 2.5f);
        Vector3 spawnPosition = new Vector2(transform.position.x, randomY);
        GameObject obstacle = Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);

        activeObstacles.Add(obstacle); 

        StartCoroutine(DestroyObstacleAfterTime(obstacle, 5f));
    }

    private IEnumerator DestroyObstacleAfterTime(GameObject obstacle, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (!isPlayerDied && obstacle != null) 
        {
            activeObstacles.Remove(obstacle);
            Destroy(obstacle);
        }
    }

    private void Player_OnPlayerDied()
    {
        isPlayerDied = true;

        foreach (GameObject obstacle in activeObstacles)
        {
            if (obstacle != null)
            {
                Destroy(obstacle.GetComponent<Collider2D>());
            }
        }

        activeObstacles.Clear(); 
    }

    private void GameManager_OnGameStarted()
    {
        isGameStarted = true;
    }
    private void GameManager_OnGameOver()
    {
        isGameStarted = false;
    }

    private void OnDestroy()
    {
        Player.OnPlayerDied -= Player_OnPlayerDied;
        GameManager.Instance.OnGameStarted -= GameManager_OnGameStarted;
        GameManager.Instance.OnGameOver -= GameManager_OnGameOver;
    }
}
