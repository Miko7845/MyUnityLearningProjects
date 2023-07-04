using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    [HideInInspector] public bool gameOver = false;
    [SerializeField] ParticleSystem explosionParticle;
    [SerializeField] ParticleSystem fireworksParticle;
    [SerializeField] AudioClip moneySound;
    [SerializeField] AudioClip explodeSound;
    [SerializeField] AudioClip bounceSound;
    [SerializeField] float floatForce;
    Rigidbody playerRb;
    AudioSource playerAudio;
    float gravityModifier = 1.5f;

    void Start()
    {
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();
        playerRb = GetComponent<Rigidbody>();
        playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !gameOver)
        {
            playerRb.AddForce(Vector3.up * floatForce, ForceMode.Impulse);
        }
        if (transform.position.y > 14.3)
        {
            transform.position = new Vector3(transform.position.x, 14.3f, transform.position.z);
            playerRb.velocity = Vector3.zero;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject);
        }
        else if(other.gameObject.CompareTag("Ground") && !gameOver)
        {
            playerAudio.PlayOneShot(bounceSound, 1.0f);
            playerRb.AddForce(Vector3.up * 100, ForceMode.Impulse);
        }
    }
}