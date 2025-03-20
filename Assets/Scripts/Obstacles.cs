using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    [SerializeField] float obstacleSpeed = 0.005f;
    private bool isPlayerDied;
    
    void Start()
    {
        Player.OnPlayerDied += Player_OnPlayerDied;
    }

    private void FixedUpdate() 
    {
        if (isPlayerDied)
        {
            return;
        }
        TranslateObstacle();
    }    
    private void TranslateObstacle()
    {
        transform.Translate(Vector2.left * obstacleSpeed);
    }
    private void Player_OnPlayerDied()
    {
        isPlayerDied = true;

        foreach (BoxCollider2D collider in gameObject.GetComponentsInChildren<BoxCollider2D>())
        {
            collider.enabled = false;
        }
    }
    private void OnDestroy() 
    {
        Player.OnPlayerDied -= Player_OnPlayerDied;
    }

}
