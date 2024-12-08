using UnityEngine;

public class RainVolumeController : MonoBehaviour
{
    public string playerTag = "Player"; // Tag do jogador
    public float maxVolume = 1f; // Volume m�ximo da chuva
    public float minVolume = 0f; // Volume m�nimo da chuva
    public float volumeFadeSpeed = 1f; // Velocidade de transi��o do volume
    private AudioSource rainAudioSource;
    private GameObject player;
    private bool isPlayerInside = false;

    void Awake()
    {
        // Encontrar o AudioSource da chuva
        rainAudioSource = GetComponent<AudioSource>();

        // Encontrar o objeto do jogador
        player = GameObject.FindGameObjectWithTag(playerTag);

        // Iniciar o volume da chuva com o m�nimo
        rainAudioSource.volume = minVolume;
    }

    void Update()
    {
        // Verificar se o jogador e a chuva foram encontrados
        if (player != null && rainAudioSource != null)
        {
            // Interpolar o volume com base em se o jogador est� dentro ou fora da chuva
            float targetVolume = isPlayerInside ? maxVolume : minVolume;
            rainAudioSource.volume = Mathf.Lerp(rainAudioSource.volume, targetVolume, Time.deltaTime * volumeFadeSpeed);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar se o jogador entrou no gatilho da chuva
        if (other.CompareTag(playerTag))
        {
            isPlayerInside = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Verificar se o jogador saiu do gatilho da chuva
        if (other.CompareTag(playerTag))
        {
            isPlayerInside = false;
        }
    }
}
