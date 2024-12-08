using UnityEngine;

public class SnowVolumeController : MonoBehaviour
{
    public string playerTag = "Player"; // Tag do jogador
    public float maxVolume = 0.5f; // Volume m�ximo da neve
    public float minVolume = 0f; // Volume m�nimo da neve
    public float volumeFadeSpeed = 1f; // Velocidade de transi��o do volume
    private AudioSource snowAudioSource;
    private GameObject player;
    private bool isPlayerInside = false;

    void Awake()
    {
        // Encontrar o AudioSource da neve
        snowAudioSource = GetComponent<AudioSource>();

        // Encontrar o objeto do jogador
        player = GameObject.FindGameObjectWithTag(playerTag);

        // Iniciar o volume da neve com o m�nimo
        snowAudioSource.volume = minVolume;
    }

    void Update()
    {
        // Verificar se o jogador e a neve foram encontrados
        if (player != null && snowAudioSource != null)
        {
            // Interpolar o volume com base em se o jogador est� dentro ou fora da neve
            float targetVolume = isPlayerInside ? maxVolume : minVolume;
            snowAudioSource.volume = Mathf.Lerp(snowAudioSource.volume, targetVolume, Time.deltaTime * volumeFadeSpeed);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar se o jogador entrou no gatilho da neve
        if (other.CompareTag(playerTag))
        {
            isPlayerInside = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Verificar se o jogador saiu do gatilho da neve
        if (other.CompareTag(playerTag))
        {
            isPlayerInside = false;
        }
    }
}
