using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 3f;     //Speed
    public float bounceForce = 5f;   //push the enemy away when it touches the player
    public float damage = 10f;       //damage dealt to the player
    public int maxHealth = 50;       //Max health of the enemy
    public int currentHealth;        //Current health of the enemy

    private Transform player;        //Reference to the player object
    private bool isBouncing = false; //Flag to check if the enemy is bouncing
    private Rigidbody2D rb;          //Reference to the Rigidbody2D of the enemy

    //Reference to the PlayerHealth script
    private PlayerHealth playerHealth;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; //Find player by tag
        
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>(); //PlayerHealth component

        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;  //Set initial health
    }

    void Update()
    {
        if (!isBouncing)
        {
            MoveTowardPlayer();
        }
    }

    void MoveTowardPlayer()
    {
        
        Vector2 direction = (player.position - transform.position).normalized;

        
        rb.velocity = direction * moveSpeed; //Move enemy toward player
    }

    //Handle collision with player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Deal damage to the player
            playerHealth.TakeDamage(damage); //TakeDamage takes a float value for health

            //bouncing away from player
            Vector2 bounceDirection = (transform.position - player.position).normalized;
            rb.AddForce(bounceDirection * bounceForce, ForceMode2D.Impulse);

            isBouncing = true; //Mark the enemy as bouncing
        }
    }

    //When the enemy stops colliding with the player
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isBouncing = false; //Stop bouncing when the enemy isn't colliding with the player
        }
    }

    //Handle taking damage from bullets or other sources
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();  //Call the Die method when the enemy's health reaches zero
        }
    }

    // Handle the enemy's death
    private void Die()
    {
        playerHealth.IncreaseEnemiesKilled();
        Debug.Log("Enemy died!");
        Destroy(gameObject);  //Destroy the enemy object
    }
}
