using UnityEngine;

public class ZenAreas : MonoBehaviour
{
    // Referências para os GameObjects Music e AudioManager.
    // Estes podem ser definidos no inspector arrastando os objetos relevantes para esses campos.
    public GameObject music;
    public GameObject audioManager;

    // Este método é chamado quando um objeto com um Collider 2D entra no trigger.
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o objeto que entrou tem a tag "ZenArea"
        if (other.CompareTag("Player"))
        {
            // Ativa o GameObject Music e desativa o AudioManager.
            music.SetActive(true);
            audioManager.SetActive(false);
        }
    }

    // Este método é chamado quando um objeto com um Collider 2D sai do trigger.
    private void OnTriggerExit2D(Collider2D other)
    {
        // Verifica se o objeto que saiu tem a tag "ZenArea"
        if (other.CompareTag("Player"))
        {
            // Desativa o GameObject Music e ativa o AudioManager.
            music.SetActive(false);
            audioManager.SetActive(true);
        }
    }
}
