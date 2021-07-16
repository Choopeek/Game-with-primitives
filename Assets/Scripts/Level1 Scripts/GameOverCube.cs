using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverCube : MonoBehaviour
{
    private float scaleX;
    private float scaleY;
    private float scaleZ;
    private float scaleSpeed;
    private float targetScale;
    private GameManager gameManager;

    public AudioClip playerDeathSound;
    private AudioSource enemySounds;
    private float soundVolumeFull = 1.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        enemySounds = GetComponent<AudioSource>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        scaleSpeed = Random.Range(1f, 5f);
        targetScale = Random.Range(26f, 41f);
        scaleX = transform.localScale.x;
        scaleY = transform.localScale.y;
        scaleZ = transform.localScale.z;
        StartCoroutine(StartGrowth());
                
    }

    // Update is called once per frame
    void Update()
    {
        
       
    }

    IEnumerator StartGrowth()
    {
        float time;
        time = Time.time;
        float time2;
        time2 = 0 - time;
        
        while(transform.localScale.z <= targetScale && gameManager.enemyGotTheBase)
        {
            //here we change the scale of the object. Fun thing is, that you can change the scale via time only by using Time.time, not Time.deltaTime. Why? fuck I know. UPDATE: Because deltatime is a nearly constant time between frames. While Time.time is a absolute value
            transform.localScale = new Vector3(scaleX, scaleY, scaleZ * scaleSpeed * (Time.time + time2));
            yield return new WaitForSeconds(Time.deltaTime); //by such a waitforseconds we can make a Coroutine work nice and smooth over time
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            enemySounds.PlayOneShot(playerDeathSound, soundVolumeFull);
        }
    }


}
