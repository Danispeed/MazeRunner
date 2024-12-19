using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnWalls : MonoBehaviour
{
    public GameObject wall;

    // ChatGPT
    public HashSet<Vector3> wallPositions = new HashSet<Vector3>(); // Track wall positions

    public spawnFloor floorSpawning;
    private int gridWidth;
    private int gridHeight;
    private float spacing;
    private int offset_heigt;
    private List<GameObject> gameObjects = new List<GameObject>();
    private int countOfObjects;
    private bool floor_finished = false;

    void Update()
    {
        if (!floor_finished){
            if (floorSpawning != null){
                gridWidth = floorSpawning.width;
                gridHeight = floorSpawning.height;
                spacing = floorSpawning.size_floor;

                offset_heigt = gridHeight / 2;

                GenerateWalls();

                floor_finished = true;
                this.enabled = false;
            }        
        }

        if (floor_finished)
        {
            StartCoroutine(RemoveWall());
        }
    }

    IEnumerator RemoveWall()
    {
        while (gameObjects.Count > 0)
        {
            yield return new WaitForSeconds(2f); 

            int indexToRemove = Random.Range(0, countOfObjects);
            GameObject wallToRemove = gameObjects[indexToRemove];
            
            if (wallToRemove != null)
            {
                Destroy(wallToRemove); 
            }
        }
    }

    void GenerateWalls()
    {
        for (int x = -gridWidth; x < gridWidth; x++)
        {
            for (int y = -gridHeight - offset_heigt; y < gridHeight; y++)
            {
            float spawnProbability = 0.01f;

            if (Random.value < spawnProbability)
            {
                Vector3 position = new Vector3(x * spacing, 2, y * spacing);
                GameObject newWall = Instantiate(wall, position, Quaternion.identity, transform);
                gameObjects.Add(newWall);

                if (Random.value > 0.5f)  
                {
                    newWall.transform.Rotate(0, 90f, 0);
                }
            }
            }
        }

        countOfObjects = gameObjects.Count;
    }
}
