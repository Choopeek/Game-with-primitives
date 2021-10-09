using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchedEnemy : MonoBehaviour


    
{
    public Vector3 startingPosition;
    public bool enemyInStartingPosition;
    private float enemyAssembleSpeed = 25f;

    public int numberOfAttacks;

    private BossScript boss;

    private Color currentColor;

    //delay before launching enemies. used in top attack and in wave attack
    public float timeBeforeLaunch;
    public float delayBeforeLaunchMin = 0.5f;
    public float delayBeforeLaunchMax = 3f;
    //secondWave helps that enemies don't launch altogether.
    public bool secondWave = false;

    #region WaveAttacks Variables
    public bool enemyWaveAttack; //here we store all variables needed for the Wave Attack
    public float enemyWaveAttackSpeed;
    private float destroyBoundZ = -32f;    
    public bool enemyWaveAttackRunning;
    #endregion

    #region TopAttacks Variables
    public bool enemyTopAttack; //here we store all variables needed for the Top Attack
    private bool enemyTopPositionPreparing;
    private float topBound = 40f;
    public float enemyTopAttackAscendSpeed;
    public float enemyTopAttackSpeed;
    private bool enemyDownPosition;
    public Vector3 topAttackPosition;
    private float waitForSecondsTopAttack = 2f;
    private float downBound = -10f;
    //needed for correct sound performance
    bool passingBy;
    int passingByBound = 6;
    #endregion


    public bool enemyColumnAttack;
    public float enemyColumnAttackSpeed;
    public Vector3 columnAttackPos;
    


    public bool enemyBurstAttack;
    public float enemyBurstAttackDelayBeforeLaunch;
    public float enemyBurstAttackSpeed;
    public Vector3 enemyBurstAttackStartPos;

    private Rigidbody enemyRB;

    EnemySoundController enemySound;

    public bool angryOnly;

    public bool bossIsDying;

    Level2GameManager gameManager;



    private void Awake()
    {
        
    }






    // Start is called before the first frame update
    void Start()
    {
        boss = GameObject.Find("BOSS").GetComponent<BossScript>();
        currentColor = GetComponent<Renderer>().material.color;
        if (!angryOnly)
        {
            enemyRB = GetComponent<Rigidbody>();
            enemySound = GetComponent<EnemySoundController>();
            enemyRB.constraints = RigidbodyConstraints.FreezeAll;
        }
        if (bossIsDying)
        {
            enemyRB = GetComponent<Rigidbody>();
        }
        gameManager = gameManager = GameObject.Find("Level2GameManager").GetComponent<Level2GameManager>();

    }

    

    public IEnumerator EnemyAssemblePosition()
    {
        while (!enemyInStartingPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, startingPosition, enemyAssembleSpeed * Time.deltaTime);
            if (transform.position == startingPosition)
            {
                enemyInStartingPosition = true;
                boss.WaitForEnemiesInPositions();
            }
            yield return null;
        }

    }

    public void LaunchEnemy() //here we launch the attack. Depending on the bool the boss gave to this script
    {
        if (enemyWaveAttack)
        {

            timeBeforeLaunch = Random.Range(delayBeforeLaunchMin, delayBeforeLaunchMax);
            StartCoroutine(StartEnemyWaveAttack(timeBeforeLaunch));
        }

        if (enemyTopAttack)
        {
            timeBeforeLaunch = Random.Range(delayBeforeLaunchMin, delayBeforeLaunchMax);
            StartCoroutine(StartEnemyTopAttack(timeBeforeLaunch));
        }

        if (enemyBurstAttack)
        {
            StartCoroutine(StartEnemyBurstAttack());
        }

        if (enemyColumnAttack)
        {
            StartCoroutine(StartEnemyColumnAttack());
        }
        
    }
    IEnumerator StartEnemyWaveAttack(float timeBeforeLaunch) //here we wait for some time before enabling enemy to move. So they won't launch alltogether
    {
        if (secondWave)
        {
            timeBeforeLaunch = timeBeforeLaunch + delayBeforeLaunchMax;
        }
        yield return new WaitForSeconds(timeBeforeLaunch);
        
        gameObject.tag = "EnemyGO";
        enemyWaveAttackRunning = true;
        enemySound.WaveAttackSound();
        while (enemyWaveAttackRunning)
        {
            transform.Translate(Vector3.back * enemyWaveAttackSpeed * Time.deltaTime);
            if (transform.position.z < destroyBoundZ)
            {
                enemyWaveAttackRunning = false;
                Destroy(gameObject);
            }
            yield return null;
        }
        
    }

    IEnumerator StartEnemyTopAttack(float timeBeforeLaunch) //boss top Attack
    {
        if (secondWave)
        {
            timeBeforeLaunch = timeBeforeLaunch + delayBeforeLaunchMax;
        }
        yield return new WaitForSeconds(timeBeforeLaunch);
        enemyTopPositionPreparing = true;
        enemySound.TopAttackSound();
        while (enemyTopPositionPreparing)
        {
            
            transform.Translate(Vector3.up * enemyTopAttackAscendSpeed * Time.deltaTime);
            if (transform.position.y  >= topBound)
            {
                enemyTopPositionPreparing = false;
            }
            yield return null;
        }

        
            transform.position = topAttackPosition;
            

        yield return new WaitForSeconds(waitForSecondsTopAttack);
        while (!enemyDownPosition)
        {
            transform.Translate(Vector3.down * enemyTopAttackSpeed * Time.deltaTime);
            if (transform.position.y <= passingByBound && !passingBy)
            {
                PlayPassingBy();
            }
            if (transform.position.y <= downBound)
            {
                enemyDownPosition = true;
                Destroy(gameObject);
            }
            yield return null;
        }
        
        
    }

    IEnumerator StartEnemyBurstAttack()
    {
        bool burstAttack = true;        
        transform.position = enemyBurstAttackStartPos;
        yield return new WaitForSeconds(enemyBurstAttackDelayBeforeLaunch);
        gameObject.tag = "EnemyGO";
        enemySound.BurstAttackSound();
        while (burstAttack)
        {
            transform.Translate(Vector3.back * enemyBurstAttackSpeed * Time.deltaTime);

            if (transform.position.z < 10 && !passingBy)
            {
                BurstPassingBy();
            }
            if (transform.position.z < destroyBoundZ)
            {
                burstAttack = false;
                Destroy(gameObject);
            }
            yield return null;
        }
        
        
    }

    IEnumerator StartEnemyColumnAttack()
    {
        
        bool columnAttack = true;
        transform.position = columnAttackPos;
        yield return new WaitForSeconds(enemyBurstAttackDelayBeforeLaunch);
        StartCoroutine(changeColumnColour());
        enemySound.ColumnAttackSound();
        while (columnAttack)
        {
            transform.Translate(Vector3.back * enemyColumnAttackSpeed * Time.deltaTime);
            if (transform.position.z < destroyBoundZ)
            {
                columnAttack = false;                
                Destroy(gameObject);
            }
            yield return null;
        }
        
    }

    IEnumerator changeColumnColour()
    {
        bool columnAttack = true;
        float colorGlower = 1;        
        while (columnAttack)
        {
            if (colorGlower >= 1)
            {
                GetComponent<Renderer>().material.color = new Color(colorGlower, 0, 0, 1);
                colorGlower = colorGlower - 0.9f;
            }
            if (colorGlower >= 0)
            {
                GetComponent<Renderer>().material.color = new Color(colorGlower, 0, 0, 1);
                colorGlower = colorGlower + 0.01f;
            }
            if (transform.position.z < destroyBoundZ)
            {
                columnAttack = false;
            }
            yield return null;
        }
        
    }

    public IEnumerator GlowEveryone()
    {
        
        if (gameObject.tag == "Enemy")
        {
            
            gameObject.tag = "GlowingEnemy";
            float currentTime = Time.time;
            float estimateTime = currentTime + 3.5f;
            bool glowEveryone = true;
            float colorGlower = 0.1f;
            while (glowEveryone)
            {
                if (colorGlower >= 1)
                {
                    GetComponent<Renderer>().material.color = new Color(colorGlower, 0, 0, 1);
                    colorGlower = colorGlower - 0.9f;
                    
                }
                if (colorGlower >= 0)
                {
                    GetComponent<Renderer>().material.color = new Color(colorGlower, 0, 0, 1);
                    colorGlower = colorGlower + 0.05f;
                }
                if (Time.time >= estimateTime)
                {
                    glowEveryone = false;
                    GetComponent<Renderer>().material.color = new Color(1, 0, 0, 1);
                    gameObject.tag = "Enemy";
                    Destroy(this);
                }
                
                yield return null;
            }
        }
        yield return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("projectile"))
        {
            
            //Debug.Log("I'm hit");
            
        }

        if (other.CompareTag("player"))
        {
            
            gameManager.PlayerIsDead();
        }
    }

    void PlayPassingBy()
    {
        if(!passingBy)
        {
            enemySound.TopPassingBySoundPlayback();
            passingBy = true;
        }
    }

    void BurstPassingBy()
    {
        if(!passingBy)
        {
            enemySound.BurstPassingByPlayback();
            passingBy = true;
        }
    }

    
        
}
