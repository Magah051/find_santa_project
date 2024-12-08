using UnityEngine;

public class DustVolumeController : MonoBehaviour
{
    public string playerTag = "Player"; // Tag do jogador
    public float maxVolume = 1f; // Volume máximo da poeira
    public float minVolume = 0f; // Volume mínimo da poeira
    public float volumeFadeSpeed = 1f; // Velocidade de transição do volume
    private AudioSource dustAudioSource;
    private GameObject player;
    private bool isPlayerInside = false;

    void Awake()
    {
        // Encontrar o AudioSource da poeira
        dustAudioSource = GetComponent<AudioSource>();

        // Encontrar o objeto do jogador
        player = GameObject.FindGameObjectWithTag(playerTag);

        // Iniciar o volume da poeira com o mínimo
        dustAudioSource.volume = minVolume;
    }

    void Update()
    {
        // Verificar se o jogador e a poeira foram encontrados
        if (player != null && dustAudioSource != null)
        {
            // Interpolar o volume com base em se o jogador está dentro ou fora da poeira
            float targetVolume = isPlayerInside ? maxVolume : minVolume;
            dustAudioSource.volume = Mathf.Lerp(dustAudioSource.volume, targetVolume, Time.deltaTime * volumeFadeSpeed);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar se o jogador entrou no gatilho da poeira
        if (other.CompareTag(playerTag))
        {
            isPlayerInside = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Verificar se o jogador saiu do gatilho da poeira
        if (other.CompareTag(playerTag))
        {
            isPlayerInside = false;
        }
    }
}
