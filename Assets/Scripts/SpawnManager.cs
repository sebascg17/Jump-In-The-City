using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaculoPrefab;
    private Vector3 spawnPos = new Vector3(45, 2, 0);
    private float startDelay = 2f;
    private PlayerController playerControllerScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnObstaculosConIntervaloAleatorio());
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    IEnumerator SpawnObstaculosConIntervaloAleatorio ()
    {
        yield return new WaitForSeconds(startDelay); // Delay inicial

        while (true)
        {
            SpawnObstaculo();

            float intervalo = Random.Range(0.8f, 4f); // intervalo aleatorio
            yield return new WaitForSeconds(intervalo);
        }
    }

    void SpawnObstaculo ()
    {
        if(playerControllerScript.gameOver == false)
        {
            int objectIndex = Random.Range(0, obstaculoPrefab.Length);
            Instantiate(obstaculoPrefab[objectIndex], spawnPos, obstaculoPrefab[objectIndex].transform.rotation);
        }
    }
}
