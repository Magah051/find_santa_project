using UnityEngine;

public class HeartsBox : MonoBehaviour
{
    public GameObject heartPrefab; // Prefab do cora��o
    public Transform heartsParent; // Parente dos cora��es
    public float heartSpacing = 2.0f; // Espa�amento entre os cora��es
    private PlayerController playerController; // Refer�ncia ao script PlayerController
    private FloatValue previousHealth; // Valor anterior da sa�de do jogador

    void Start()
    {
        // Encontra o script PlayerController na cena
        playerController = FindObjectOfType<PlayerController>();

        if (playerController == null)
        {
            Debug.LogError("PlayerController n�o encontrado na cena.");
            return;
        }

        // Atualiza o valor anterior da sa�de do jogador
        previousHealth = playerController.PlayerCurrentHealth;

        UpdateHearts();
    }

    void Update()
    {
        // Verifica se houve mudan�as na vida do jogador para atualizar os cora��es
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
        // Limpa os cora��es anteriores
        foreach (Transform child in heartsParent)
        {
            Destroy(child.gameObject);
        }

        // Obt�m a posi��o inicial do primeiro cora��o com base na posi��o do heartsParent
        Vector3 parentPosition = heartsParent.position;
        Vector3 heartPosition = new Vector3(parentPosition.x - (heartSpacing * (Mathf.CeilToInt(previousHealth.RuntimeValue) - 1) / 2), parentPosition.y, parentPosition.z);

        // Instancia novos cora��es com base na vida atual do jogador
        for (int i = 0; i < Mathf.CeilToInt(previousHealth.RuntimeValue); i++)
        {
            // Instancia o cora��o
            GameObject newHeart = Instantiate(heartPrefab, heartsParent);

            // Define a posi��o do cora��o
            newHeart.transform.position = heartPosition;

            // Atualiza a posi��o para o pr�ximo cora��o
            heartPosition.x += heartSpacing;

        }
    }
}