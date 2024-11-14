using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;  //speed
    private Vector2 direction; //Direction the bullet will move in
    public float damage = 10f; //Damage dealt to enemy

    //method is called when the bullet is instantiated
    public void Fire(Vector2 targetDirection)
    {
        direction = targetDirection.normalized; //Normalize direction for consistent speed
    }

    void Update()
    {
        //Move the bullet in the direction it's facing
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //If the bullet hits an object tagged as "Enemy"
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //Get the Enemy script component
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            if (enemy != null)
            {
                //Apply damage to the enemy
                enemy.TakeDamage((int)damage);
                Destroy(gameObject); //Destroy the bullet after it hits
            }
        }
    }
}
