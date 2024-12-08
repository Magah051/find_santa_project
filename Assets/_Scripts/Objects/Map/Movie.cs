using UnityEngine;

public class Movie : MonoBehaviour
{
    public Transform target; // O objeto que você deseja seguir (geralmente o jogador)

    void LateUpdate()
    {
        if (target != null)
        {
            // Siga a posição do alvo, mas mantenha a mesma altura do minimapa
            Vector3 newPosition = target.position;
            newPosition.y = transform.position.y;
            transform.position = newPosition;

            // Mantenha a mesma rotação do alvo
            transform.rotation = Quaternion.Euler(90f, target.eulerAngles.y, 0f);
        }
    }
}
