using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public int maxHealth = 50;
    public int currentHealth;
    public int attackDamage = 5;
    public float attackRange = 1f;

    private Transform player;
    private Rigidbody2D rb;
    private Animator _ani;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        _ani = GetComponent<Animator>();

        currentHealth = maxHealth;
    }

    void Update()
    {
        // Enemy movement towards the player
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * moveSpeed;
        }


        // Face the player
        // transform.right = direction;

        // Check if in attack range
        if (player != null)
        {
            if (Vector2.Distance(transform.position, player.position) <= attackRange)
            {
                Attack();
            }
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().TakeDamage(attackDamage);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            // Enemy defeated
            Destroy(gameObject);
        }
    }
    public void DieWithHook()
    {
        Destroy(gameObject);

    }
    void Attack()
    {
        _ani.SetTrigger("attack");

        // Implement ranged attack logic
        player.GetComponent<PlayerController>().TakeDamage(attackDamage);

    }
}
