using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public float moveSpeed = 5f; //Speed

    private Vector2 movement; //direction of movement
    private Transform m_transform;

    void Start()
    {
        m_transform = this.transform;
    }

    void Update()
    {
        //Get input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Normalize movement
        movement = movement.normalized;
    }

    void FixedUpdate()
    {
        //Transform the movement from world space to local space
        Vector2 localMovement = m_transform.InverseTransformDirection(movement); //Converts to local space

        // Apply movement to the player's position
        transform.Translate(localMovement * moveSpeed * Time.fixedDeltaTime);
    }
}
