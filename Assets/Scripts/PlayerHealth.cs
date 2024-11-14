using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f; //starting health
    public GameObject gameOverScreen; //Game Over screen
    public Text enemiesKilledText;  //enemies killed text
    public Text timeAliveText;     //time alive text
    public Button restartButton;   //Restart button

    private bool isDead = false; //track if player is dead
    private float timeAlive = 0f; //how long player has been alive
    private int enemiesKilled = 0; //enemies killed

    void Update()
    {
        if (!isDead)
        {
            timeAlive += Time.deltaTime; //Increment time alive
        }

    }

    //Method to take damage
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            Die();
        }
    }

    //Handle the player's death
    private void Die()
    {
        isDead = true;
        ShowGameOverScreen();
        PauseGame(); //Pause the game when the player dies
    }

    //Show the Game Over screen
    private void ShowGameOverScreen()
    {
    gameOverScreen.SetActive(true);

    //Convert timeAlive to minutes and seconds
    int minutes = Mathf.FloorToInt(timeAlive / 60);
    int seconds = Mathf.FloorToInt(timeAlive % 60);

    //Display time alive
    timeAliveText.text = string.Format("Time Alive: {0:00}:{1:00}", minutes, seconds);

    //Display the number of enemies killed
    enemiesKilledText.text = "Enemies Killed: " + enemiesKilled;

    //Enable the restart button
    restartButton.gameObject.SetActive(true);
    }

    //Method to increase the number of enemies killed
    public void IncreaseEnemiesKilled()
    {
        enemiesKilled++;
    }

    //Restart the game
    public void RestartGame()
    {
        isDead = false;
        health = 100f;
        timeAlive = 0f;
        enemiesKilled = 0;
        gameOverScreen.SetActive(false); //Hide screen

        
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);

        UnpauseGame(); //Unpause
    }

    //Pause the game
    private void PauseGame()
    {
        Time.timeScale = 0f; //Stops time
    }

    //Unpause the game
    private void UnpauseGame()
    {
        Time.timeScale = 1f; //Resumes time
    }
}
