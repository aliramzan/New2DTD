using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public int maxHealth = 100;
    public int currentHealth;
    public int healthPickupAmount = 20;
    public int meleeDamage = 10;
    public float meleeRange = 1f;
    public float hookRange = 5f;
    public LayerMask hookableLayers;
    public float hookPullForce = 10f;
    public int hookManaCost = 20;
    public int manaRegenRate = 2;
    public int maxMana = 100;
    public int currentMana;
    private Rigidbody2D rb;
    private Animator p_ani;
    bool Hook;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        p_ani = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        // Handle player input and movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(horizontalInput, verticalInput).normalized * moveSpeed;
        rb.velocity = movement;

        // Hook mechanic
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, hookRange, hookableLayers);
        Debug.DrawRay(transform.position, transform.right * hookRange, Color.red);
        if (hit.collider != null && Hook && currentMana >= hookManaCost)
        {
            Debug.Log("Grapple hook hit: " + hit.collider.gameObject.name);

            Rigidbody2D hookedRb = hit.collider.GetComponent<Rigidbody2D>();
            if (hookedRb != null)
            {
                Debug.Log("Applying hook force to: " + hookedRb.gameObject.name);
                hit.collider.transform.position = Vector3.MoveTowards(hit.collider.transform.position, this.transform.position, 1f);
               
            }
            else
            {
                Debug.LogWarning("Rigidbody not found on hooked object: " + hit.collider.gameObject.name);
            }
        }
        else
        {
            Debug.LogWarning("Grapple hook missed.");
        }
     

        // Mana regeneration
        currentMana = Mathf.Clamp(currentMana + manaRegenRate, 0, maxMana);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HealthPickup"))
        {
            RestoreHealth(healthPickupAmount);
            Destroy(other.gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            // Player defeated
            // You can implement game over logic here
            GameManager.instance.GameOver.SetActive(true);
            Destroy(gameObject);
            Time.timeScale = 0;
        }
    }

    public void RestoreHealth(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    }

    public void MeleeAttack()
    {
        p_ani.SetTrigger("attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, meleeRange);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                enemy.GetComponent<EnemyController>().TakeDamage(meleeDamage);
            }
        }
    }

    public void GrappleHook()
    {
        Hook = true;
    }

}


