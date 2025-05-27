using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;

    public ParticleSystem explosionParticle;
    public ParticleSystem dirtyParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;

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
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();

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
        if(Input.GetKeyDown(KeyCode.Space) && isOnGround == true && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtyParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
    }

    private void OnCollisionEnter ( Collision collision )
    {
        if(collision.gameObject.CompareTag("Suelo"))
        {
            isOnGround = true;
            dirtyParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstaculos"))
        {
            gameOver = true;
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1); // Random death animation
            playerAudio.PlayOneShot(crashSound, 1.0f);
            explosionParticle.Play();
            dirtyParticle.Stop();
            GameObject.Find("UIManager").GetComponent<UIManager>().ShowGameOver();
        }       
    }
}
