using UnityEngine;
using UnityEngine.UI;

public class BookInventoryController : MonoBehaviour
{
    // Referência para o GameObject BookInventory
    public GameObject bookInventory;
    // Referência para o componente de texto que você deseja modificar

    private void Start()
    {
    }

    void Update()
    {
        // Verifica se a tecla "B" foi pressionada
        if (Input.GetKeyDown(KeyCode.B))
        {
            // Ativa ou desativa o GameObject BookInventory com base no estado atual
            bookInventory.SetActive(!bookInventory.activeSelf);
        }
    }
}
