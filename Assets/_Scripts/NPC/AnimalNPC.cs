using UnityEngine;

public class AnimalNPC : MonoBehaviour
{
    public Transform[] waypoints;
    public float moveSpeed = 3f;
    public Animator animator;
    private int currentWaypointIndex = 0;
    private Transform currentWaypoint;
    private bool facingRight = true;
    private bool isCollidingWithPlayer = false; // Flag para controlar a colisão com o jogador
    public GameObject UI;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        animator.SetBool("isRunning", true);
        if (waypoints.Length > 0)
        {
            currentWaypoint = waypoints[currentWaypointIndex];
        }
        else
        {
            Debug.LogError("No waypoints assigned to AnimalNPC.");
            enabled = false; // Disable the script if no waypoints are assigned
        }
    }

    void Update()
    {
        if (!isCollidingWithPlayer) // Só move se não estiver colidindo com o jogador
        {
            MoveToWaypoint();
        }
    }

    void MoveToWaypoint()
    {
        animator.SetBool("isRunning", true);
        if (currentWaypoint != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, currentWaypoint.position, moveSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, currentWaypoint.position) < 0.1f)
            {
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
                currentWaypoint = waypoints[currentWaypointIndex];

                if (currentWaypoint.position.x < transform.position.x && facingRight)
                {
                    Flip();
                }
                else if (currentWaypoint.position.x > transform.position.x && !facingRight)
                {
                    Flip();
                }
            }
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    // Detecta a colisão com o jogador
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isCollidingWithPlayer = true;
            animator.SetBool("isRunning", false);
            GameObject UI_Entity = Instantiate(UI, transform.position, Quaternion.identity);
            Destroy(UI_Entity, 7f);
        }
    }


    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isCollidingWithPlayer = false;
        }
    }
}
