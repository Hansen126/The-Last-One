using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject[] enemyPrefabs;
    public float spawnInterval = 10.0f;
    public float spawnRange = 10.0f;

    public Player player;

    private void Start()
    {
        if (enemyPrefabs == null || enemyPrefabs.Length == 0)
        {
            Debug.LogError("Enemy prefabs array is not set or empty. Please assign prefabs in the inspector.");
            return;
        }

        StartCoroutine(SpawnEnemyRoutine());
    }

    private IEnumerator SpawnEnemyRoutine()
    {
        while (player.isDead == false)
        {
            if(player.timeSurvived > 0)
            {
                spawnInterval = 4.0f;
            }
            if(player.timeSurvived > 30)
            {
                spawnInterval = 3.5f;
            }
            if (player.timeSurvived > 60)
            {
                spawnInterval = 1.0f;
            }
            if (player.timeSurvived > 90)
            {
                spawnInterval = 2.5f;
            }
            if (player.timeSurvived > 120)
            {
                spawnInterval = 1.0f;
            }
            if (player.timeSurvived > 150)
            {
                spawnInterval = 1.5f;
            }
            if (player.timeSurvived > 180)
            {
                spawnInterval = 1.0f;
            }
            if (player.timeSurvived > 210)
            {
                spawnInterval = 1.3f;
            }
            if (player.timeSurvived > 240)
            {
                spawnInterval = 1.0f;
            }
            if (player.timeSurvived > 270)
            {
                spawnInterval = 1.0f;
            }
            yield return new WaitForSeconds(spawnInterval);
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPosition = GetRandomSpawnPosition();

        if (enemyPrefabs.Length == 0)
        {
            Debug.LogError("Enemy prefabs array is empty.");
            return;
        }

        int enemyIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject enemyObject = Instantiate(enemyPrefabs[enemyIndex], spawnPosition, Quaternion.identity, transform);

        Player playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (playerScript != null)
        {
            Enemy enemy = enemyObject.GetComponent<Enemy>();
            playerScript.RegisterEnemy(enemy);
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float screenAspect = Camera.main.aspect;
        float screenHeight = Camera.main.orthographicSize * 2;
        float screenWidth = screenHeight * screenAspect;

        int direction = Random.Range(0, 4);
        Vector3 spawnPosition = Vector3.zero;
        float offsetY = 2.5f;

        switch (direction)
        {

            case 0: // Spawn dari atas
                spawnPosition = new Vector3(Random.Range(-screenWidth / 2, screenWidth / 2), Camera.main.orthographicSize - offsetY, 0);
                break;
            case 1: // Spawn dari kiri
                spawnPosition = new Vector3(-screenWidth / 2, Random.Range(-Camera.main.orthographicSize + offsetY, Camera.main.orthographicSize - offsetY), 0);
                break;
            case 2: // Spawn dari kanan
                spawnPosition = new Vector3(screenWidth / 2, Random.Range(-Camera.main.orthographicSize + offsetY, Camera.main.orthographicSize - offsetY), 0);
                break;
            case 3: // Spawn dari bawah 
                spawnPosition = new Vector3(Random.Range(-screenWidth / 2, screenWidth / 2), -Camera.main.orthographicSize, 0);
                break;
        }

        return spawnPosition;
    }

}
