using UnityEngine;

public class PlayerLookAtCursor : MonoBehaviour
{
    public GameObject bulletPrefab;  //Reference the bullet prefab
    public Transform bulletSpawnPoint;  // bullet spawn

    private Transform m_transform;

    void Start()
    {
        m_transform = this.transform;
    }

    private void LookAtMouse()
    {
        //Get the world position of the mouse cursor
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = m_transform.position.z;  //Set z position to match player

        //Calculate direction from player to mouse position
        Vector2 direction = mousePosition - m_transform.position;

        //Calculate the angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        //Apply rotation to the player
        m_transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }

    void Update()
    {
        LookAtMouse();

        //Fire a bullet on left-click
        if (Input.GetMouseButtonDown(0))  // Left-click
        {
            ShootBullet();
        }
    }

    void ShootBullet()
    {
        //direction of bullet
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - m_transform.position).normalized;  //Direction from player to mouse

        //Spawn the bullet at player
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);

        //Fire the bullet
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.Fire(direction);
    }
}
