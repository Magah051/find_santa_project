using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public bool IsEnergizing = false;
    public Animator animator;
    private float fadeDuration = 0.5f; // Duration for the fade effect
    private Renderer objectRenderer;
    private Color originalColor;
    private AudioSource audioSource; // AudioSource para reproduzir o som
    private bool isPlaying = false; // Flag para controlar se o áudio está sendo reproduzido
    public AudioClip yourAudioClip; // Clip de áudio a ser reproduzido


    private void Awake()
    {
        // Obter o componente Animator do GameObject
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found!");
        }

        // Obtém ou adiciona um AudioSource ao GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Obter o componente Renderer do GameObject
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer == null)
        {
            Debug.LogError("Renderer component not found!");
        }
        else
        {
            // Obter a cor original do material
            originalColor = objectRenderer.material.color;
        }
        // Configura o AudioClip no AudioSource
        audioSource.clip = yourAudioClip;
        audioSource.loop = true;
    }


    // Method called when another Collider enters into contact with this Collider (if marked as Trigger)
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the Collider that entered into contact is a player
        if (other.CompareTag("Player"))
        {
            // Set IsEnergizing to true
            IsEnergizing = true;

            // Check if there is an assigned Animator component
            if (animator != null)
            {
                // Modify the IsEnergizing variable in the Animator to true
                animator.SetBool("IsEnergizing", true);
                FadeInAnimation();
                // Reproduzir o som se não estiver sendo reproduzido
                if (!isPlaying)
                {
                    audioSource.Play();
                    isPlaying = true;
                }
            }
            else
            {
                Debug.LogError("Animator component is missing!");
            }
        }
    }

    // Method called when another Collider ceases contact with this Collider (if marked as Trigger)
    void OnTriggerExit2D(Collider2D other)
    {
        // Check if the Collider that exited the contact is a player
        if (other.CompareTag("Player"))
        {
            // Set IsEnergizing to false
            IsEnergizing = false;

            // Check if there is an assigned Animator component
            if (animator != null)
            {
                // Modify the IsEnergizing variable in the Animator to false
                animator.SetBool("IsEnergizing", false);
                FadeOutAnimation();
                // Parar o som se estiver sendo reproduzido
                if (isPlaying)
                {
                    audioSource.Stop();
                    isPlaying = false;
                }
            }
            else
            {
                Debug.LogError("Animator component is missing!");
            }
        }
    }

    // Function to handle the fade-in animation
    private void FadeInAnimation()
    {
        animator.CrossFade("zenEnergyRecovereEffect", fadeDuration);
    }

    // Function to handle the fade-out animation
    private void FadeOutAnimation()
    {
        animator.CrossFade("zenEnergyRecovereEffect", fadeDuration);
    }
}
