using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    [SerializeField] private float jumpForce = 10f;
    private Rigidbody2D rigidBody2D;
    private bool isAlive = false;
    public static event Action OnPlayerDied;
    public static event Action OnPlayerScored;

    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        rigidBody2D.freezeRotation = true; 
        rigidBody2D.gravityScale = 0;

        GameManager.Instance.OnGameStarted += GameManager_OnGameStarted;
        GameManager.Instance.OnGameOver += GameManager_OnGameOver;
    }

    void Update()
    {
        if (!isAlive)
        {
            return;
        }
        Jump();
        Fall();
    }

    private void GameManager_OnGameStarted()
    {
        isAlive = true;
        rigidBody2D.velocity = Vector2.zero;
        rigidBody2D.gravityScale = 4;
    }

    private void GameManager_OnGameOver()
    {
        isAlive = false;
        rigidBody2D.velocity = Vector2.zero;
    }

    public void Jump()
    {
        if (!isAlive)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            rigidBody2D.velocity = Vector2.up * jumpForce;
            transform.rotation = Quaternion.Euler(0, 0, 30f); 
        }
    }

    private void Fall()
    {
        if (rigidBody2D.velocity.y < 0) 
        {
            float fallSpeed = Mathf.Clamp(-rigidBody2D.velocity.y, 0, 10);
            float targetAngle = Mathf.Lerp(0, -90f, fallSpeed / 15f);
            transform.rotation = Quaternion.Euler(0, 0, targetAngle);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (!isAlive)
        {
            return;
        }
        isAlive = false;
        OnPlayerDied?.Invoke();
        StartCoroutine(RotateOnDeath());
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        OnPlayerScored?.Invoke();
    }

    private IEnumerator RotateOnDeath()
    {
        float duration = 0.3f; 
        float elapsedTime = 0f;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(0, 0, -90f); 

        while (elapsedTime < duration)
        {
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = endRotation; 
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnGameStarted -= GameManager_OnGameStarted;
        GameManager.Instance.OnGameOver -= GameManager_OnGameOver;
    }
}
