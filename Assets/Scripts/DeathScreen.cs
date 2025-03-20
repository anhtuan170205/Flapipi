using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour
{
    private Image deathScreenImage;
    private float fadeDuration = 0.1f;
    private float displayDuration = 0.05f;

    private void Start()
    {
        deathScreenImage = GetComponent<Image>();
        deathScreenImage.color = new Color(0, 0, 0, 0);
        Player.OnPlayerDied += Player_OnPlayerDied;
    }
    private void Player_OnPlayerDied()
    {
        StartCoroutine(FadeIn());
    }
    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        Color color = deathScreenImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
            deathScreenImage.color = color;
            yield return null;
        }
        deathScreenImage.color = new Color(0, 0, 0, 1);

        yield return new WaitForSeconds(displayDuration);

        elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            color.a = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            deathScreenImage.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        deathScreenImage.color = new Color(0, 0, 0, 0);
    }
    private void OnDestroy()
    {
        Player.OnPlayerDied -= Player_OnPlayerDied;
    }
}
