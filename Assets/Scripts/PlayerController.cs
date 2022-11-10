using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;
    private AudioSource backgroundAudio;
    public GameObject mainCamera;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public bool doubleJump = true;
    public float jumpForce = 600.0f;
    public float doubleJumpForce = 300.0f;
    public float gravityModifier = 1.5f;
    public bool doubleSpeed = false;
    public bool isOnGround = true;
    public bool gameOver = false;
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        backgroundAudio = mainCamera.GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && (isOnGround || doubleJump) && !gameOver){
            doubleJump = isOnGround ? true : false;
            float currForce = isOnGround ? jumpForce : doubleJumpForce;

            playerRb.AddForce(Vector3.up * currForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
        else if(Input.GetKey(KeyCode.LeftShift)){
            doubleSpeed = true;
            playerAnim.SetFloat("Speed_Multiplier_f", 2.0f);
        }
        else if(doubleSpeed){
            doubleSpeed = false;
            playerAnim.SetFloat("Speed_Multiplier_f", 1.0f);
        }

    }

    private void OnCollisionEnter(Collision collision){
        if(gameOver)
            return;

        if(collision.gameObject.CompareTag("Ground")){
            isOnGround = true;
            dirtParticle.Play();
        }
        else if(collision.gameObject.CompareTag("Obstacle")){
            Debug.Log("Game Over!");
            gameOver = true;
            dirtParticle.Stop();
            backgroundAudio.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);
            explosionParticle.Play();
            playerAnim.SetBool("Death_b",true);
            playerAnim.SetInteger("DeathType_int",1);
        }
    }
}
