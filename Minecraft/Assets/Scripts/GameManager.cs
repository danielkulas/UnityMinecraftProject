using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform cubePrefab;
    [SerializeField] private int worldSize = 10;
    private const float OFFSET = 0.5f;

    private void Start()
    {
        Cursor.visible = false; //DZ!
        Cursor.lockState = CursorLockMode.Locked; //DZ!
        //Cursor.lockState = CursorLockMode.None; //DZ!
        //Cursor.visible = true; //DZ!
        spawnObjects();
    }

    private void spawnObjects()
    {
        float randXOffset = Random.Range(-10.0f, 10.0f);
        float randYOffset = Random.Range(-10.0f, 10.0f);

        for(int i = -worldSize; i < worldSize; i++)
        {
            for(int j = -worldSize; j < worldSize; j++)
            {
                float noOfCubesAt = Mathf.PerlinNoise(i/10.0f + randXOffset, j/10.0f + randYOffset) * 6.0f;

                for(int a = 0, posY = 1; a < noOfCubesAt; a++, posY++)
                {
                    Vector3 pos = new Vector3(i, posY+OFFSET, j);
                    Instantiate(cubePrefab, pos, Quaternion.identity);
                }
            }
        }
    }
}