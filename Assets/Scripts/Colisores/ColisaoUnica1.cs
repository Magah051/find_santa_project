using UnityEngine;

public class ColisaoUnica1 : MonoBehaviour
{
    private bool colisaoOcorreu = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificar se a colisão já ocorreu
        if (!colisaoOcorreu)
        {
            // Executar o código relacionado à colisão aqui

            // Desativar colliders após a primeira colisão
            DesativarColliders();

            // Definir a flag de colisão como true para evitar colisões subsequentes
            colisaoOcorreu = true;
        }
    }

    private void DesativarColliders()
    {
        // Desativar todos os colliders anexados ao GameObject (pode ser adaptado conforme necessário)
        Collider2D[] colliders = GetComponents<Collider2D>();

        foreach (Collider2D collider in colliders)
        {
            collider.enabled = false;
        }
    }
}
