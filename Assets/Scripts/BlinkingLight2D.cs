using UnityEngine;

public class BlinkingLight2D : MonoBehaviour
{
    private UnityEngine.Rendering.Universal.Light2D light2D;
    public float blinkInterval = 0.5f; // Intervalo de piscada em segundos
    public float minIntensity = 0f; // Intensidade mínima (apagado)
    public float maxIntensity = 1f; // Intensidade máxima (aceso)

    private float timer;
    private bool isOn = true;

    void Start()
    {
        light2D = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        if (light2D != null)
        {
            light2D.intensity = maxIntensity;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= blinkInterval)
        {
            isOn = !isOn;
            light2D.intensity = isOn ? maxIntensity : minIntensity;
            timer = 0f; // Reseta o timer
        }
    }
}
