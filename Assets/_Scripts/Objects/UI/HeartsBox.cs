using UnityEngine;

public class HeartsBox : MonoBehaviour
{
    public GameObject heartPrefab; // Prefab do coração
    public Transform heartsParent; // Parente dos corações
    public float heartSpacing = 2.0f; // Espaçamento entre os corações
    private PlayerController playerController; // Referência ao script PlayerController
    private FloatValue previousHealth; // Valor anterior da saúde do jogador

    void Start()
    {
        // Encontra o script PlayerController na cena
        playerController = FindObjectOfType<PlayerController>();

        if (playerController == null)
        {
            Debug.LogError("PlayerController não encontrado na cena.");
            return;
        }

        // Atualiza o valor anterior da saúde do jogador
        previousHealth = playerController.PlayerCurrentHealth;

        UpdateHearts();
    }

    void Update()
    {
        // Verifica se houve mudanças na vida do jogador para atualizar os corações
        FloatValue currentHealth = playerController.PlayerCurrentHealth;
        if (currentHealth.RuntimeValue != previousHealth.RuntimeValue)
        {
            previousHealth = currentHealth;
            UpdateHearts();
        }
        Debug.Log(currentHealth.RuntimeValue);
        Debug.Log(previousHealth.RuntimeValue);
        UpdateHearts();
    }

    void UpdateHearts()
    {
        // Limpa os corações anteriores
        foreach (Transform child in heartsParent)
        {
            Destroy(child.gameObject);
        }

        // Obtém a posição inicial do primeiro coração com base na posição do heartsParent
        Vector3 parentPosition = heartsParent.position;
        Vector3 heartPosition = new Vector3(parentPosition.x - (heartSpacing * (Mathf.CeilToInt(previousHealth.RuntimeValue) - 1) / 2), parentPosition.y, parentPosition.z);

        // Instancia novos corações com base na vida atual do jogador
        for (int i = 0; i < Mathf.CeilToInt(previousHealth.RuntimeValue); i++)
        {
            // Instancia o coração
            GameObject newHeart = Instantiate(heartPrefab, heartsParent);

            // Define a posição do coração
            newHeart.transform.position = heartPosition;

            // Atualiza a posição para o próximo coração
            heartPosition.x += heartSpacing;

        }
    }
}