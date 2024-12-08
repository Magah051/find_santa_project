using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueControl : MonoBehaviour
{
    [Header("Components")]
    public GameObject dialogueObj;
    public Text speechText;
    public Text actorNameText;

    [Header("Settings")]
    public float typingSpeed;

    private string[] sentences;
    private int index;
    private Coroutine typingCoroutine; // Para rastrear a corrotina em execu??o

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            NextSentence();
        }
    }

    public void Speech(string[] txt, string actorName)
    {
        // Para qualquer corrotina anterior e limpa o texto
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        dialogueObj.SetActive(true);
        speechText.text = ""; // Limpa o texto ao iniciar
        actorNameText.text = actorName;
        sentences = txt;
        index = 0;

        // Inicia a corrotina para digitar a primeira frase
        typingCoroutine = StartCoroutine(TypeSentence());
    }

    IEnumerator TypeSentence()
    {
        // Garante que o texto comece vazio
        speechText.text = "";

        // Digita cada letra da frase atual
        foreach (char letter in sentences[index].ToCharArray())
        {
            speechText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        // Corrotina conclu?da
        typingCoroutine = null;
    }

    public void NextSentence()
    {
        // Verifica se a frase atual foi completamente exibida
        if (speechText.text == sentences[index])
        {
            if (index < sentences.Length - 1)
            {
                index++;
                speechText.text = ""; // Limpa o texto antes da pr?xima frase

                // Para corrotinas anteriores e inicia uma nova
                if (typingCoroutine != null)
                {
                    StopCoroutine(typingCoroutine);
                }
                typingCoroutine = StartCoroutine(TypeSentence());
            }
            else
            {
                EndDialogue(); // Finaliza o di?logo
            }
        }
    }

    public void hidePainel()
    {
        // Para corrotinas e reseta o di?logo
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        speechText.text = ""; // Limpa o texto
        actorNameText.text = ""; // Opcional: limpa o nome do ator
        index = 0;
        dialogueObj.SetActive(false); // Esconde o painel
    }

    private void EndDialogue()
    {
        speechText.text = "";
        index = 0;
        dialogueObj.SetActive(false);
    }
}