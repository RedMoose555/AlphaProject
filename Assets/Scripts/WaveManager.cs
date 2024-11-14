using UnityEngine;
using UnityEngine.UI;
using TMPro;  // Add this line
using System.Collections;

public class WaveManager : MonoBehaviour
{
    public GameObject enemyPrefab;             //enemy prefab
    public Transform spawnArea;                //area where enemies will spawn
    public TextMeshProUGUI waveText;           //UI Text
    public int baseEnemyCount = 1;             //enemies for the first wave

    private int waveNumber = 1;                //wave number

    void Start()
    {
        StartCoroutine(StartWave());
    }

    //Coroutine to handle each wave
    IEnumerator StartWave()
    {
        while (true)
        {
            //Display the wave number
            waveText.text = "Wave: " + waveNumber;
            waveText.enabled = true;
            
            //Wait to show the wave number
            yield return new WaitForSeconds(2f);
            
            waveText.enabled = false;

            //Spawn enemies
            SpawnEnemies(waveNumber);

            //Wait until all enemies are defeated before starting the next wave
            yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Enemy").Length == 0);

            //Move to the next wave
            waveNumber++;

            //Update the base enemy count for each wave
            baseEnemyCount += 3;
        }
    }

    //Spawns enemies randomly within the spawn area
    void SpawnEnemies(int waveNumber)
    {
        int enemyCount = baseEnemyCount;  //Number of enemies to spawn

        for (int i = 0; i < enemyCount; i++)
        {
            //Generate a random position within the spawn area
            Vector2 spawnPosition = new Vector2(
                Random.Range(spawnArea.position.x - spawnArea.localScale.x / 2, spawnArea.position.x + spawnArea.localScale.x / 2),
                Random.Range(spawnArea.position.y - spawnArea.localScale.y / 2, spawnArea.position.y + spawnArea.localScale.y / 2)
            );

            //Instantiate the enemy at the random position
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
