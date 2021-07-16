using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //this section gives the move speed and rotations values
    private float forwardSpeed = 5.0f;
    private float rotateSpeed = 5.0f;
    private float backwardSpeed = 5.0f;
    private float upDownSpeed = 4.0f;
    //component variables
    private Rigidbody playerRB;
    //here we implement shooting ability
    public GameObject projectilePrefab;
    private bool shotNotFired = false;
    public int shotExists;
    //player area limiters here, but the ingame walls now work correctly. But I'll leave those just in case
    private float leftBound = -12.0f;
    private float rightBound = 12.0f;
    private float upBound = 7.15f;
    private float downBound = -5.5f;
    //implement sound effects
    public AudioClip shootSound;
    public AudioClip reloadSound;
    public AudioClip hitSound;
    private AudioSource playerAudio;
    private float volumeSoundFull = 1.0f;

    private GameManager gameManager;

    public ParticleSystem playerExplodeParticle;


    
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        //getting the component(s) for the Player object
        playerRB = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
    }

    
    void Update()
    {
        //negate force. So when the player collides it wont make some chaos in controls
        playerRB.velocity = Vector3.zero;
        playerRB.angularVelocity = Vector3.zero;

        
        //Controls
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * forwardSpeed * Time.deltaTime);

        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * backwardSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime * 20); 
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.back * rotateSpeed * Time.deltaTime * 20);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * upDownSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * upDownSpeed * Time.deltaTime);
        }
        //here we track if the player made a shot. 
        shotExists = GameObject.FindObjectsOfType<projectile>().Length;
        if (shotExists == 0)
        {
            
            shotNotFired = true;
        }
        if (shotExists == 1)
        {
            
            shotNotFired = false;
        }

        
        //if there is no bullet and SPACE is pressed - the shot is fired
        if (Input.GetKeyDown(KeyCode.Space) && shotNotFired)
        {
            playerAudio.PlayOneShot(shootSound, volumeSoundFull);
            Vector3 playerPos = transform.position;
            Vector3 playerDirection = transform.up;
            Quaternion playerRotation = transform.rotation;
            float spawnDistance = 1f;
            Vector3 firePos = playerPos + playerDirection * spawnDistance;
            Instantiate(projectilePrefab, firePos, playerRotation);
            //here we calculate the position in front of the player. player rotation is included.
        }

        //implement bounds here
        if (transform.position.x < leftBound)
        {
            transform.position = new Vector3(leftBound, transform.position.y, transform.position.z);
        }
        if (transform.position.x > rightBound)
        {
            transform.position = new Vector3(rightBound, transform.position.y, transform.position.z);
        }
        if (transform.position.y > upBound)
        {
            transform.position = new Vector3(transform.position.x, upBound, transform.position.z);
        }
        if (transform.position.y < downBound)
        {
            transform.position = new Vector3(transform.position.x, downBound, transform.position.z);
        }
    }

    //here we kill the player if he collides with the enemy
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            gameManager.enemyKilledPlayer = true;
            gameManager.GameOver();
            Instantiate(playerExplodeParticle, transform.position, playerExplodeParticle.transform.rotation);
            Destroy(gameObject);
        }
    }
}
