using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager : MonoBehaviour
{
    private bool isPlayerDied;
    private void Start()
    {
        isPlayerDied = false;
        Player.OnPlayerDied += Player_OnPlayerDied;
    }
    private void Update()
    {
        if (isPlayerDied)
        {
            return;
        }
        transform.Translate(Vector2.left * 0.005f);
        if (transform.position.x < -17.5)
        {
            transform.position = new Vector3(14, -6, 0);
        }
    }
    private void Player_OnPlayerDied()
    {
        isPlayerDied = true;
    }
}
