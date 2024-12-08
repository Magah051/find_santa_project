using UnityEngine;
using UnityEngine.UI;

public class BookInventoryController : MonoBehaviour
{
    // Refer�ncia para o GameObject BookInventory
    public GameObject bookInventory;
    // Refer�ncia para o componente de texto que voc� deseja modificar

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
