using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public string[] speechText;
    public string actorName;

    private DialogueControl dc;
    bool onRadious;
    private bool isDialogueActive = false;

    public LayerMask playerLayer;
    public float radious;

    void Start()
    {
        dc = FindObjectOfType<DialogueControl>();
    }

    private void FixedUpdate()
    {
        Interact();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && onRadious && !isDialogueActive)
        {
            StartDialogue();
        }
    }

    private void StartDialogue()
    {
        isDialogueActive = true; // Marca o di�logo como ativo
        dc.Speech(speechText, actorName);
        Debug.Log($"Iniciando di�logo com {actorName}");
    }

    private void EndDialogue()
    {
        isDialogueActive = false; // Permite iniciar o di�logo novamente
        dc.hidePainel();
        Debug.Log("Di�logo finalizado.");
    }

    public void Interact()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radious, playerLayer);
        if(hit != null)
        {
            if (!onRadious)
            {
                Debug.Log("Jogador entrou no raio de intera��o.");
            }

            onRadious = true;

        }
        else
        {
            if (onRadious)
            {
                Debug.Log("Jogador saiu do raio de intera��o.");
            }

            onRadious = false;

            // Finaliza o di�logo ao sair do raio, se necess�rio
            if (isDialogueActive)
            {
                EndDialogue();
            }
        }

    }
}
