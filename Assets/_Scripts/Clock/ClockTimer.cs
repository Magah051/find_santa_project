using UnityEngine;
using UnityEngine.UI; // Para manipular UI
using UnityEngine.SceneManagement; // Para carregar cenas

public class ClockTimer : MonoBehaviour
{
    public Text clockText; // Arraste o componente de texto da UI aqui
    private int hours = 23;
    private int minutes = 50;
    private int seconds = 0;

    private float timer; // Controla o tempo acumulado entre atualizações de segundos

    void Start()
    {
        UpdateClockUI(); // Inicializa o texto do relógio na UI
    }

    void Update()
    {
        timer += Time.deltaTime;

        // Atualiza o relógio a cada segundo
        if (timer >= 1f)
        {
            timer = 0f; // Reseta o contador de tempo
            UpdateTime();
        }
    }

    void UpdateTime()
    {
        // Incrementa os segundos
        seconds++;
        if (seconds >= 60)
        {
            seconds = 0;
            minutes++;
        }

        if (minutes >= 60)
        {
            minutes = 0;
            hours++;
        }

        // Se atingir 00:00:00
        if (hours == 0 && minutes == 0 && seconds == 0)
        {
            LoadGameOverScene();
        }

        UpdateClockUI();
    }

    void UpdateClockUI()
    {
        // Atualiza o texto na UI no formato HH:MM:SS
        clockText.text = $"{hours:00}:{minutes:00}:{seconds:00}";
    }

    void LoadGameOverScene()
    {
        // Substitua "GameOver" pelo nome da sua cena de Game Over
        SceneManager.LoadScene(1);
    }
}
