using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2PlayerController : MonoBehaviour
{
    private float playerSpeed = 3f;
    private float playerRightBound = 1.94f;
    private float playerLeftBound = -4.93f;
    [SerializeField] ParticleSystem deathParticle;

    [SerializeField] GameObject projectilePlayers;

    //sounds are stored here
    private AudioSource playerSounds;
    [SerializeField] AudioClip shootSound;

    [SerializeField] DialogueManager dialogueControls;
    public bool inDialogue;

    float timeSinceLastShot;

    [SerializeField] GameObject rpgLeft;
    [SerializeField] GameObject rpgRight;

    public bool rpgFireMode;
        
    void Start()
    {
        playerSounds = GetComponent<AudioSource>();
        
    }

    
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) | Input.GetKey(KeyCode.A) & !inDialogue)
        {
            transform.Translate(Vector3.left * playerSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.RightArrow) | Input.GetKey(KeyCode.D) & !inDialogue)
        {
            transform.Translate(Vector3.right * playerSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space) & !inDialogue)
        {
            if (!rpgFireMode)
            {
                ShootABullet();
            }
            if (rpgFireMode)
            {
                ShootRPG();
            }
            
        }

        //implement bounds for player so he can't get off screen

        if (transform.position.x > playerRightBound)
        {
            transform.position = new Vector3(playerRightBound, transform.position.y, transform.position.z); 
        }

        if (transform.position.x < playerLeftBound)
        {
            transform.position = new Vector3(playerLeftBound, transform.position.y, transform.position.z);
        }

        if (Input.GetKeyUp(KeyCode.Space) & inDialogue)
        {
            dialogueControls.DisplayNextSentence();
        }
    }

    void ShootABullet()
    {
        
        if (!ShotIsFired())
        {
            
            CreateABullet();
            playerSounds.PlayOneShot(shootSound, 1f);
        }
        
    }

    bool ShotIsFired() //we check if the shot was fired. So player cant shoot again while the bullet is present. UPDATE: now the player can shoot every second. or faster if the bullet is not present on the scene.
    {
        GameObject shotWasFired;
        GameObject.Find("projectilePlayer");
        shotWasFired = GameObject.Find("projectilePlayer(Clone)");
        float fireRate = 1f;
        if (rpgFireMode)
        {
            fireRate = 0.5f;
        }
        float nextShotAvailable = Time.time;
        if (shotWasFired == null || nextShotAvailable >= timeSinceLastShot + fireRate)
        {
            
            return false;
        }
        else
        {
            
            return true;
        }
    }

    void CreateABullet()
    {
        timeSinceLastShot = Time.time;
        float bulletSafeSpace = 0.8f;
        float projectileZPos = transform.position.z + bulletSafeSpace;
        Instantiate(projectilePlayers, new Vector3(transform.position.x, transform.position.y, projectileZPos), transform.rotation);
    }

    void ShootRPG()
    {
        if (!ShotIsFired())
        {
            timeSinceLastShot = Time.time;
            playerSounds.PlayOneShot(shootSound, 1f);
            float bulletSafeSpace = 0.8f;
            float XposAdjustment = 0.32f;
            float YposAdjustment = 0.4f;
            float projectileZPos = transform.position.z + bulletSafeSpace;
            Instantiate(projectilePlayers, new Vector3(transform.position.x, transform.position.y, projectileZPos), transform.rotation);
            //right RPG
            float projectileRightXpos = transform.position.x + XposAdjustment;
            float projectileRightYpos = transform.position.y + YposAdjustment;
            Instantiate(projectilePlayers, new Vector3(projectileRightXpos, projectileRightYpos, projectileZPos), transform.rotation);

            //left RPG
            float projectileLeftXpos = transform.position.x - XposAdjustment;
            float projectileLeftYpos = transform.position.y + YposAdjustment;
            Instantiate(projectilePlayers, new Vector3(projectileLeftXpos, projectileLeftYpos, projectileZPos), transform.rotation);

        }

    }
    public IEnumerator EnlargeYourRPG()
    {
        rpgFireMode = true;
        rpgLeft.gameObject.SetActive(true);
        rpgRight.gameObject.SetActive(true);
          

        yield return null;
    }

    public void Death()
    {
        Instantiate(deathParticle, transform.position, deathParticle.transform.rotation);        
        Destroy(gameObject);
    }
}
