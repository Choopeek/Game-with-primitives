using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2PlayerController : MonoBehaviour
{
    private float playerSpeed = 3f;
    private float playerRightBound = 1.94f;
    private float playerLeftBound = -4.93f;

    [SerializeField] GameObject projectilePlayers;

    //sounds are stored here
    private AudioSource playerSounds;
    [SerializeField] AudioClip shootSound;

    [SerializeField] DialogueManager dialogueControls;
    public bool inDialogue;
    
        
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
            
            ShootABullet();
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

    bool ShotIsFired() //we check if the shot was fired. So player cant shoot again while the bullet is present
    {
        GameObject shotWasFired;
        GameObject.Find("projectilePlayer");
        shotWasFired = GameObject.Find("projectilePlayer(Clone)"); 
        if (shotWasFired == null)
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
        float bulletSafeSpace = 0.8f;
        float projectileZPos = transform.position.z + bulletSafeSpace;
        Instantiate(projectilePlayers, new Vector3(transform.position.x, transform.position.y, projectileZPos), transform.rotation);
    }

    
}
