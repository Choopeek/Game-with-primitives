using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float enemyInPosition = -0.4f;
    private bool enemySpawned = false;
    private bool moveToTarget = false;
    private float enemySpeed;
    private float enemySpeedMin = 0.8f;
    private float enemySpeedMax = 1.2f;
    private float enemyFallSpeed;
    private float enemyFallSpeedMin = 1.0f;
    private float enemyFallSpeedMax = 3.0f;
    public GameObject playerBase;
    
    private Rigidbody enemyRB;
    

    //sounds

    public AudioClip captureBase;
    public AudioClip landed;
    public AudioClip falling;
    public AudioClip killedPlayer;
    private AudioSource enemySounds;
    public ParticleSystem playerExplodeParticle;
    private float soundVolumeHalf = 0.6f;
    private float soundVolumeFull = 1.0f;

    //reffering to GameManager so we could pass the info
    private GameManager gameManager;


    

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        enemySounds = GetComponent<AudioSource>();
        enemyFallSpeed = Random.Range(enemyFallSpeedMin, enemyFallSpeedMax);
        enemySpeed = Random.Range(enemySpeedMin, enemySpeedMax);
        enemyRB = GetComponent<Rigidbody>();
        playerBase = GameObject.Find("PlayerBase");
        StartCoroutine(SpawningEnemy());
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator SpawningEnemy()
    {
        //here the enemy falls down from the sky. When they reach the bottom (enemyInPosition value) it start the MoveToTarget coroutine
        while (!enemySpawned)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, enemyInPosition), enemyFallSpeed * Time.deltaTime);
            if (transform.position.z >= enemyInPosition)
            {
                enemySpawned = true;
            }
            yield return null;
        }
        //print("Enemy ready for fight."); //dont need it no more
        enemySounds.PlayOneShot(landed, soundVolumeHalf);       
        StartCoroutine(MoveToTarget());
        StopCoroutine(SpawningEnemy());
    }
    
    //here we move the ENEMy to the PLAYERBASE
    public IEnumerator MoveToTarget ()
    {
       moveToTarget = true;
       while (moveToTarget)
        {
            //dumb thing is: you can't assign PlayerBaseObj from Hierarchy as a playerBasePosition. You need to assign it from the prefabs
            transform.position = Vector3.MoveTowards(transform.position, playerBase.transform.position, enemySpeed * Time.deltaTime);
            yield return null;
            if (Vector3.Distance(transform.position, playerBase.transform.position) < 0.1f) //checking the distance between ENEMY and PLAYERBASE
            {
                //print("WeGotTheBase"); //dont need it no more everything works just fine now
                enemySounds.PlayOneShot(captureBase, soundVolumeFull);
                gameManager.enemyGotTheBase = true;
                gameManager.GameOver();
                moveToTarget = false;
                
            }
        }
        StopCoroutine(MoveToTarget());

    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            enemySounds.PlayOneShot(killedPlayer, soundVolumeFull);
            Instantiate(playerExplodeParticle, other.transform.position, playerExplodeParticle.transform.rotation);
            gameManager.enemyKilledPlayer = true;
            gameManager.GameOver();
        }
    }




}
