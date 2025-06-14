﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerX : MonoBehaviour
{
    public GameObject[] objectPrefabs;
    private float spawnDelay = 2;
    private float spawnInterval;

    private PlayerControllerX playerControllerScript;

    // Start is called before the first frame update
    void Start ()
    {
        StartCoroutine(SpawnObstaculosConIntervaloAleatorio());
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerControllerX>();
    }
    IEnumerator SpawnObstaculosConIntervaloAleatorio ()
    {
        yield return new WaitForSeconds(spawnDelay); // Delay inicial

        while (true)
        {
            SpawnObjects ();

            spawnInterval = Random.Range(0.5f, 3f); // intervalo aleatorio
            yield return new WaitForSeconds(spawnInterval);
        }
    }
    

    // Spawn obstacles
    void SpawnObjects ()
    {
        // Set random spawn location and random object index
        Vector3 spawnLocation = new Vector3(40, Random.Range(5, 15), 0);
        int index = Random.Range(0, objectPrefabs.Length);

        // If game is still active, spawn new object
        if (!playerControllerScript.gameOver)
        {
            Instantiate(objectPrefabs[index], spawnLocation, objectPrefabs[index].transform.rotation);
        }

    }
}
