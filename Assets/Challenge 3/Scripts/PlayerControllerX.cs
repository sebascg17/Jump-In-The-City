using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver;

    private float floatForce = 2000f;
    private float gravityModifier = 1f;
    public float heightLimit = 10f;
    private Rigidbody playerRb;

    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    private AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;
    public AudioClip groundSound;


    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        playerRb = GetComponent<Rigidbody>();

        // Solo modificamos la gravedad una vez

        Physics.gravity = new Vector3(0, -9.81f * gravityModifier, 0);

        // Apply a small upward force at the start of the game
        //playerRb.AddForce(Vector3.up * floatForce, ForceMode.Impulse);

    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        bool isBelowHeightLimit = playerRb.position.y < heightLimit;

        // While space is pressed and player is low enough, float up
        if ((Input.GetKey(KeyCode.Space) || Input.GetKeyDown(KeyCode.Space)) && !gameOver && isBelowHeightLimit)
        {
            playerRb.AddForce(Vector3.up * floatForce, ForceMode.Force);
        }

    }

    private void OnCollisionEnter(Collision other)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Suelo"))
        {
            playerAudio.PlayOneShot(groundSound, 1.0f);
            playerRb.AddForce(Vector3.up * 500, ForceMode.Impulse);
        }
        else if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Destroy(other.gameObject);// Destruye la bomba
            GameObject.Find("UIManager").GetComponent<UIManager>().ShowGameOver();

            StartCoroutine(DestroyPlayerAfterExplosion());
        } 

        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject);
        }

    }

    private IEnumerator DestroyPlayerAfterExplosion ()
    {
        // Espera el tiempo que dura la explosión
        yield return new WaitForSeconds(0.5f);

        Destroy(gameObject); // Destruye el jugador
    }

}
