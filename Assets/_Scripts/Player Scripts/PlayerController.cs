using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public enum PlayerState
{
    idle,
    walk,
    attack,
    interact,
    stagger
} 



public class PlayerController : MonoBehaviour
{
    [HideInInspector] private Rigidbody2D PlayerRigidbody;
    [HideInInspector] public Animator PlayerAnimator;
    private Vector3 Change;
    private float DeathEffectDelay = 1f;
    


    public GameObject DeathEffect;
    public AudioSource stepSound;
    public Signals ScreenKickSignal;
    public float Speed;

    [Header("Passive Variables: ")]
    public PlayerState PlayerCurrentState;
    public FloatValue PlayerInitialHealth;
    public FloatValue PlayerCurrentHealth;
    //public VectorValue PlayerStartingPosition;
    public Signals PlayerHealthSignal;
    public string whereIam;
    private List<string> currentLocations = new List<string>();

    [Header("Inventory Variables: ")]
    public Inventory PlayerInventory;
    public SpriteRenderer ReceivedItemSprite;

    [Header("GameStat: ")]
    public GameStatEnum GameStat;

    [Header("UI: ")]
    public bool isWalking = false; // Define se o jogador está caminhando ou não
    public float energyDecreaseRate = 0.1f; // Taxa de diminuição da energia
    private Slider zenEnergySlider; // Referência ao ZenEnergySlider
    private float currentEnergy = 1; // Energia atual do jogador
    private bool previousIsWalking; // Estado anterior de isWalking
    public float energyIncreaseRate = 0.1f; // Taxa de aumento da energia
    private bool isInZenEnergyField; // Indica se o jogador está dentro do campo de energia Zen
    public GameObject HightTemperature;
    public GameObject NormalTemperature;
    public GameObject LowTemperature;

    public static PlayerController Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        //PlayerPrefs.DeleteAll();
        if (PlayerPrefs.HasKey("PlayerData"))
        {
              LoadPlayer();
        }

        PlayerCurrentState = PlayerState.walk;
        PlayerRigidbody = GetComponent<Rigidbody2D>();
        PlayerAnimator = GetComponent<Animator>();
        PlayerAnimator.SetFloat("MoveX", 0);
        PlayerAnimator.SetFloat("MoveY", -1);
        PlayerInitialHealth.InitialValue = 5;
        PlayerCurrentHealth.InitialValue = 5;
        PlayerInitialHealth.RuntimeValue = 5;
        PlayerCurrentHealth.RuntimeValue = 5;
            }

    void Update()
    {
        // Verifica se a tecla L foi pressionada
        if (Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene(0);
            //PlayerPrefs.DeleteAll();
        }
       
        if (isWalking)
        {
            if (!stepSound.isPlaying)
            {
                stepSound.Play();
            }
        }
        else
        {
            if (stepSound.isPlaying)
            {
                stepSound.Stop();
            }
        }
    }
    public float GetCurrentHealth()
    {
        return PlayerCurrentHealth.RuntimeValue;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("FireLand") || other.CompareTag("SnowLand") || other.CompareTag("ForestLand"))
        {
            whereIam = other.tag.ToLower();
        }
        else if (other.CompareTag("ZenEnergy"))
        {
            isInZenEnergyField = true;
        }

        if (other.CompareTag("Save"))
        {
            SavePlayer();
            Debug.Log("Jogo salvo!");
        }

    }
    public class PlayerData
    {
        public float health;
        public float initialHealth;
        public float zenEnergy;
        public float positionX;
        public float positionY;
        public float positionZ;
    }
    public void SavePlayer()
    {
        PlayerData data = new PlayerData();
        data.health = PlayerCurrentHealth.RuntimeValue;
        data.initialHealth = PlayerInitialHealth.RuntimeValue;
        data.zenEnergy = currentEnergy;
        data.positionX = transform.position.x;
        data.positionY = transform.position.y;
        data.positionZ = transform.position.z;

        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("PlayerData", json);
        PlayerPrefs.Save();
    }

    public void LoadPlayer()
    {
        string json = PlayerPrefs.GetString("PlayerData");
        if (!string.IsNullOrEmpty(json))
        {
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);
            PlayerCurrentHealth.RuntimeValue = data.health;
            PlayerInitialHealth.RuntimeValue = data.initialHealth;
            currentEnergy = data.zenEnergy;
            Vector3 position = new Vector3(data.positionX, data.positionY, data.positionZ);
            transform.position = position;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("FireLand") || other.CompareTag("SnowLand") || other.CompareTag("ForestLand"))
        {
            string exitedLocation = other.tag.ToLower();
            if (currentLocations.Contains(exitedLocation))
            {
                currentLocations.Remove(exitedLocation);
                if (currentLocations.Count > 0)
                {
                    whereIam = currentLocations[currentLocations.Count - 1];
                }
                else
                {
                    whereIam = ""; // Se saiu de todos os locais
                }
            }
        }
        else if (other.CompareTag("ZenEnergy"))
        {
            isInZenEnergyField = false;
        }
    }

    void FixedUpdate()
    {
        if (PlayerCurrentState == PlayerState.interact) { return; }         // Is the player in an Interaction ? 

        // if not .. then play !
        Change = Vector3.zero;
        Change.x = Input.GetAxisRaw("Horizontal");
        Change.y = Input.GetAxisRaw("Vertical");
        if (Input.GetButtonDown("attack") && PlayerCurrentState != PlayerState.attack && PlayerCurrentState != PlayerState.stagger)
        {
            StartCoroutine(AttackCo());
        }
        else if (PlayerCurrentState == PlayerState.walk || PlayerCurrentState == PlayerState.idle)
        {
            UpdatePlayerAnimationAndMove();
        }

    }

    void UpdatePlayerAnimationAndMove()
    {
        if (Change != Vector3.zero)
        {
            MovePlayer();
            PlayerAnimator.SetFloat("MoveX", Change.x);
            PlayerAnimator.SetFloat("MoveY", Change.y);
            PlayerAnimator.SetBool("Moving", true);
            isWalking = true;
        }
        else
        {
            PlayerAnimator.SetBool("Moving", false);
            isWalking = false;
        }
    }
    public void MovePlayer()
    {
        Change.Normalize();
        PlayerRigidbody.MovePosition(transform.position + Change * Speed * Time.deltaTime);
        
    }

    public void Knock(float KnockTime, float Damage) // initial the knockback and Damage code ..
    {
        FindObjectOfType<AudioManager>().PlaySound("Hurt");

        PlayerCurrentHealth.RuntimeValue -= Damage;
        PlayerHealthSignal.Raise();

        if (PlayerCurrentHealth.RuntimeValue > 0)  // only if the player is not dead already XD .. 
        {
            StartCoroutine(KnockCo(KnockTime));
        } else
        {
            this.gameObject.SetActive(false);
            PlayerDeathEffect();
            // i can send a signal for GameOver here ! 
        }
    }

    public void PlayerDeathEffect()
    {
        FindObjectOfType<AudioManager>().PlaySound("Death");
        GameObject effect = Instantiate(DeathEffect, transform.position, Quaternion.identity);
        Destroy(effect, DeathEffectDelay);

    }


    private IEnumerator KnockCo(float KnockTime)    // Knocking back the player ..
    {
        ScreenKickSignal.Raise();
        if (PlayerRigidbody != null)
        {
            yield return new WaitForSeconds(KnockTime);
            PlayerRigidbody.velocity = Vector2.zero;
            PlayerCurrentState = PlayerState.idle;
            PlayerRigidbody.velocity = Vector2.zero;
        }
    }
    private IEnumerator AttackCo()
    {
        FindObjectOfType<AudioManager>().PlaySound("Sword");
        PlayerAnimator.SetBool("Attacking", true);          // transition to the Attack state in the Animator .. 
        PlayerCurrentState = PlayerState.attack;              // set the current state to Attack ..
        yield return null;                              // wait for 1 frame ..
        PlayerAnimator.SetBool("Attacking", false);         // since i did a transition from Any State to Attacking, this will stop the loop of Attacking to Attacking ..
        yield return new WaitForSeconds(.2f);           // wait for animation to end (0.3 sec) .. 
        if (PlayerCurrentState != PlayerState.interact)  // is the player is in multiple page interaction ..
        {
            PlayerCurrentState = PlayerState.walk;                // reset the state to walk ..
        }
    }

    public void ShowFoundItem()
    {
        if (PlayerInventory.ShowenItem != null)
        {
            if (PlayerCurrentState != PlayerState.interact)
            {
                PlayerAnimator.SetBool("ShowingItem", true);
                PlayerCurrentState = PlayerState.interact;
                ReceivedItemSprite.sprite = PlayerInventory.ShowenItem.ItemSprite;

            }
            else
            {
                PlayerAnimator.SetBool("ShowingItem", false);
                PlayerCurrentState = PlayerState.idle;
                ReceivedItemSprite.sprite = null;
                PlayerInventory.ShowenItem = null;
            }
        }
    }
}
