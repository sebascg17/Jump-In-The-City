using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float speed = 20f; // Speed of the object
    private PlayerController playerControllerScript;
    private float leftBound = -15f; // Left boundary for the object

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerScript.gameOver == false)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }

        if (transform.position.x < leftBound && gameObject.CompareTag("Obstaculos"))
        {
            Destroy(gameObject);
        }
    }
}
