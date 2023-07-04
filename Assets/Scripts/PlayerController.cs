using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public Vector3 positionToMoveTo;
    [HideInInspector] public bool gameOver = false;
    [HideInInspector] public float score = 0;
    [SerializeField] ParticleSystem explosionParticle;
    [SerializeField] ParticleSystem dirtParticle;
    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip crashSound;
    [SerializeField] float jumpForce;
    [SerializeField] float gravityModifier;
    Rigidbody playerRb;
    Animator playerAnim;
    AudioSource playerAudio;
    int jumpCount = 0;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
        LerpFunction();
        playerAnim.speed = .5f;
        StartCoroutine(IntroPause());
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !gameOver && jumpCount < 2 && positionToMoveTo.x >= transform.position.x)
        {
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpCount++;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0;
            dirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            Debug.Log("Game Over! Your score: " + (int)score);
            explosionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
        }
    }

    IEnumerator IntroPause()
    {
        yield return new WaitForSeconds(3f);
        playerAnim.speed = 1f;
    }

    IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = transform.position;
        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
    }

    void LerpFunction()
    {
        StartCoroutine(LerpPosition(positionToMoveTo, 3));
    }
}