using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float jumpForce = 10;
    public float gravityModifier = 2;
    public bool isOnGround = true;
    public bool gameOver = false;

    private static bool gravityModified = false;
    private static Vector3 originalGravity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRb = GetComponent<Rigidbody>(); 

        // Solo modificamos la gravedad una vez
        if (!gravityModified)
        {
            originalGravity = Physics.gravity;
            Physics.gravity *= gravityModifier;
            gravityModified = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isOnGround == true)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }
    }

    private void OnCollisionEnter ( Collision collision )
    {
        if(collision.gameObject.CompareTag("Suelo"))
        {
            isOnGround = true;
        }
        else if (collision.gameObject.CompareTag("Obstaculos"))
        {
            gameOver = true;
            Debug.Log("Game Over");

            GameObject.Find("UIManager").GetComponent<UIManager>().ShowGameOver();
        }       
    }
}
