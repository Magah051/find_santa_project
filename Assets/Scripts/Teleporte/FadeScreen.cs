using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeScreen : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1f;
    public float teleportOffset = 1f; // Define o deslocamento ap�s o teleporte

    private void Awake()
    {
        if (fadeImage != null)
        {
            fadeImage.enabled = true;
        }
    }

    public IEnumerator FadeAndTeleport(Transform player, Vector3 targetPosition)
    {
        yield return Fade(1); // Fade in

        // Calcula a nova posi��o com um deslocamento para evitar colis�o imediata
        Vector3 offsetPosition = targetPosition + (player.right * teleportOffset);
        player.position = offsetPosition; // Teleporta o player para a posi��o ajustada

        yield return Fade(0); // Fade out
    }

    private IEnumerator Fade(float targetAlpha)
    {
        float speed = Mathf.Abs(fadeImage.color.a - targetAlpha) / fadeDuration;
        while (!Mathf.Approximately(fadeImage.color.a, targetAlpha))
        {
            float newAlpha = Mathf.MoveTowards(fadeImage.color.a, targetAlpha, speed * Time.deltaTime);
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, newAlpha);
            yield return null;
        }
    }
}
