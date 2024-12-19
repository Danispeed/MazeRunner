using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnFloor : MonoBehaviour
{
    public GameObject floor;
    public int width;
    public int height;
    public float size_floor = 0.9f;
    void Start()
    {
        for (int x = -width; x < width; x++)
        {
            for (int y = -width; y < height; y++)
            {
                Vector3 position = new Vector3(x * size_floor, 0, y * size_floor);

                Instantiate(floor, position, Quaternion.identity, transform);
            }
        }
    }
}
