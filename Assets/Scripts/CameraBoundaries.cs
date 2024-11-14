using UnityEngine;

public class CameraBounds : MonoBehaviour
{
    public Transform player;     //The player object to constrain
    private Vector2 screenBounds; //Screen boundaries based on camera size
    private float objectWidth;    //Width of the player object
    private float objectHeight;   //Height of the player object

    void Start()
    {
        //Get the screen bounds based on camera size
        Camera mainCamera = Camera.main;
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));

        // Calculate half of the player's width and height
        objectWidth = player.GetComponent<SpriteRenderer>().bounds.extents.x;
        objectHeight = player.GetComponent<SpriteRenderer>().bounds.extents.y;
    }

    void LateUpdate()
    {
        //Get the player's current position
        Vector3 viewPos = player.position;

        //Clamp the player's position within the screen bounds
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + objectWidth, screenBounds.x - objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 + objectHeight, screenBounds.y - objectHeight);

        //Apply the clamped position to the player
        player.position = viewPos;
    }
}
