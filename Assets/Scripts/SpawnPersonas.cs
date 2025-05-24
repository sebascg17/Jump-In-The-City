using UnityEngine;

public class SpawnPersonas : MonoBehaviour
{
    public GameObject[] personasPrefab;
    private Vector3 spawnPos = new Vector3(35, 0, 2);
    private float startDelay = 2;
    private float repeatRate;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start ()
    {
        repeatRate = Random.Range(1, 3);
        InvokeRepeating("SpawnObstaculo", startDelay, repeatRate);
    }

    void SpawnObstaculo ()
    {
        int objectIndex = Random.Range(0, personasPrefab.Length);
        Instantiate(personasPrefab[objectIndex], spawnPos, personasPrefab[objectIndex].transform.rotation);
    }
}
