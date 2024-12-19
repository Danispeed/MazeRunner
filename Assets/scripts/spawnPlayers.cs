using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnPlayers : MonoBehaviour
{
    public spawnFloor floorSpawning;
    public spawnWalls wallsSpawning;

    void Start()
    {
        // Retrieving the correct prefab for both players
        SpawnPlayerPrefab(gameManager.Instance.player1SelectedPrefab);
        SpawnPlayerPrefab(gameManager.Instance.player2SelectedPrefab);
    }

    void SpawnPlayerPrefab(GameObject playerPrefab)
    {
        if (playerPrefab == null)
        {
            Debug.LogError("player prefab is null, something is wrong");
            return;
        }

        Vector3 spawnPosition = GetRandomFloorPosition();

        // while player not spawned inside of a wall
        while (wallsSpawning.wallPositions.Contains(spawnPosition))
        {
            spawnPosition = GetRandomFloorPosition();
        }

        Instantiate(playerPrefab, spawnPosition + Vector3.up * 4f, Quaternion.identity);
    }

    Vector3 GetRandomFloorPosition()
    {
        int gridWidth = floorSpawning.width;
        int gridHeight = floorSpawning.height;

        int x = Random.Range(-gridWidth, gridWidth);
        int z = Random.Range(-gridHeight, gridHeight);

        x = x / 2;
        z = z / 2;

        return new Vector3(x, 0, z);
    }
}