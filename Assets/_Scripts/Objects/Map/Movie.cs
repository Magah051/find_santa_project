using UnityEngine;

public class Movie : MonoBehaviour
{
    public Transform target; // O objeto que voc� deseja seguir (geralmente o jogador)

    void LateUpdate()
    {
        if (target != null)
        {
            // Siga a posi��o do alvo, mas mantenha a mesma altura do minimapa
            Vector3 newPosition = target.position;
            newPosition.y = transform.position.y;
            transform.position = newPosition;

            // Mantenha a mesma rota��o do alvo
            transform.rotation = Quaternion.Euler(90f, target.eulerAngles.y, 0f);
        }
    }
}
