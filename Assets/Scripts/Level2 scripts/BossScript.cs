using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{

    
    private LaunchedEnemy launchedEnemy;
    private Level2GameManager gameManager;
    private GameObject[] enemies;
    [SerializeField] private GameObject[] enemyStartingPosition;
    [SerializeField] private GameObject[] enemyToLaunch = new GameObject[12];



    #region Attack Type Variables
    private bool startWaveAttack;

    private bool startTopAttack;
    [SerializeField] GameObject[] enemyTopAttackStartPos;

    private bool startColumnAttack;

    private bool startBurstAttack;
    private int minZeroNumber = 0;    
    private float minBurstDelay = 0.1f;
    #endregion

    private Vector3[] initialBossUnitsPosition;
    private Vector3 bossDropDownPosition = new Vector3(-2.69f, 0, 50.66f);

    #region Difficulty Changer Variables
    //values for difficulty changer. Gonna implement him later.
    public float enemyWaveAttackSpeed = 10f;
    public float enemyTopAttackAscendSpeed = 20f;
    public float enemyTopAttackSpeed = 10f;
    public float enemyColumnAttackSpeed = 10f;
    public float enemyBurstAttackSpeed = 30f;
    public float delayBetweenSubWaves = 3f;
    public float delayBetweenWaves = 3f;
    #endregion
        

    #region WaveLauncherTracker

    int subWaveAttackNumber = 0;
    int waveType = 0;
    public int waveNumber = 0;

    #endregion

    #region soundVariables
    //sound variables
    private AudioSource bossSound;
    //
    [SerializeField] AudioClip topAttackSound;
    [SerializeField] AudioClip columnAttackSound;
    [SerializeField] AudioClip waveAttackSound;
    [SerializeField] AudioClip burstAttackSound;
    [SerializeField] AudioClip bossAngrySound;
    [SerializeField] AudioClip bossDeathSound;
    [SerializeField] AudioClip bossAssemblySound;
    [SerializeField] AudioClip bossAssemblyFINISHSound;
    [SerializeField] AudioClip bossDecendSound;
    [SerializeField] AudioClip bossInPositionSound;
    float soundVolumeFull = 1.0f;
    float soundVolumeHalf = 0.5f;
    float soundVolumeQuarter = 0.25f;

    #endregion 





    void Start()
    {
        gameManager = GameObject.Find("Level2GameManager").GetComponent<Level2GameManager>();        
        bossSound = GetComponent<AudioSource>();

        
        

    }

    #region BossAssembly Functions
    public IEnumerator BossAssemble()
    {
        
       //well for fucks sake
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        initialBossUnitsPosition = new Vector3[enemies.Length];
        for (int i = 0; i < enemies.Length; i++)
        {
            initialBossUnitsPosition[i] = enemies[i].transform.position;
            enemies[i].transform.position = bossDropDownPosition;
        }
        yield return new WaitForFixedUpdate();
        StartCoroutine(BossAssembleStage1());
        
    }

    IEnumerator BossAssembleStage1()
    {
        bossSound.PlayOneShot(bossInPositionSound, soundVolumeFull);
        yield return new WaitForSeconds(2.5f);
        bool assembled = false;
        bool assemblySound = false;
        bossSound.PlayOneShot(bossAssemblySound, soundVolumeQuarter);
        
        while (!assembled)
        {
            int allAssembled = 0;
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].transform.position = Vector3.MoveTowards(enemies[i].transform.position, initialBossUnitsPosition[i], 5f * Time.deltaTime);
                if (enemies[i].transform.position == initialBossUnitsPosition[i])
                {
                    allAssembled++;
                }
                if (allAssembled >= 1100 && !assemblySound)
                {
                    assemblySound = true;
                    bossSound.PlayOneShot(bossAssemblyFINISHSound, soundVolumeFull);
                }
                if (allAssembled == enemies.Length)
                {
                    assembled = true;
                    gameManager.bossIsAssembled();
                }
            }
            
            yield return null;

        }
        //AttackManager();
        //WaveLauncherTracker();

    }
    #endregion

    #region BossAttack Functions

    //new Attack Management function
    public void WaveLauncherTracker()
    {
        
        if (subWaveAttackNumber >= 6 || subWaveAttackNumber <= 0)
        {
           
            FormNewWave();            
            return;
        }
        
        if (waveType == 1)
        {
            LaunchWaveType1();
            return;
        }

        if (waveType == 2)
        {
            LaunchWaveType2();
            return;
        }
    }

    private void SetForUsualAttack()
    {
        
        startBurstAttack = false;
        startColumnAttack = false;
        startTopAttack = false;
        startWaveAttack = true;
            
    }

    private void SetForSpecialAttack()
    {
        
        int AttackType = Random.Range(1, 4);
        if (AttackType == 1)
        {
            startBurstAttack = true;
            startColumnAttack = false;
            startTopAttack = false;
            startWaveAttack = false;
        }
        if (AttackType == 2)
        {
            startBurstAttack = false;
            startColumnAttack = true;
            startTopAttack = false;
            startWaveAttack = false;
        }
        if (AttackType == 3)
        {
            startBurstAttack = false;
            startColumnAttack = false;
            startTopAttack = true;
            startWaveAttack = false;
        }

    }

    private void LaunchWaveType1()
    {
        if (subWaveAttackNumber == 1)
        {
            SetForUsualAttack();
            subWaveAttackNumber++;
            StartNewAttack();
            return;
        }
        if (subWaveAttackNumber == 2)
        {
            SetForSpecialAttack();
            subWaveAttackNumber++;
            StartNewAttack();
            return;
        }
        if (subWaveAttackNumber == 3)
        {
            SetForUsualAttack();
            subWaveAttackNumber++;
            StartNewAttack();
            return;
        }
        if (subWaveAttackNumber == 4)
        {
            SetForSpecialAttack();
            subWaveAttackNumber++;
            StartNewAttack();
            return;
        }
        if (subWaveAttackNumber == 5)
        {
            SetForUsualAttack();
            subWaveAttackNumber++;
            StartNewAttack();
            return;
        }
        
    }
    private void LaunchWaveType2()
    {
        if (subWaveAttackNumber == 1)
        {
            SetForUsualAttack();
            subWaveAttackNumber++;
            StartNewAttack();
            return;
        }
        if (subWaveAttackNumber == 2)
        {
            SetForUsualAttack();
            subWaveAttackNumber++;
            StartNewAttack();
            return;
        }
        if (subWaveAttackNumber == 3)
        {
            SetForSpecialAttack();
            subWaveAttackNumber++;
            StartNewAttack();
            return;
        }
        if (subWaveAttackNumber == 4)
        {
            SetForUsualAttack();
            subWaveAttackNumber++;
            StartNewAttack();
            return;
        }
        if (subWaveAttackNumber == 5)
        {
            SetForUsualAttack();
            subWaveAttackNumber++;
            StartNewAttack();
            return;
            
        }
        
    }

    

    private void FormNewWave()
    {
        waveType = Random.Range(1, 3);        
        subWaveAttackNumber = 1;        
        gameManager.WaveTracker();        
        Debug.Log("WaveNumber is " + waveNumber);
        
    }
    

    //this bunch of methods makes the Boss to send his parts as enemies
    private void StartNewAttack() //here we find all ENEMY tagged objects. Form an array out of them. Then we choose the enemy to be launched and untag it. So we won't choose him twice.
    {

        
        for (int i = 0; i < enemyToLaunch.Length; i++)
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");            
            enemyToLaunch[i] = enemies[Random.Range(minZeroNumber, enemies.Length)];
            enemyToLaunch[i].tag = "EnemyWaitingToBeLaunched";
            //in this function we give our enemies the position and rotation before enabling them to move towards the player.
            enemyToLaunch[i].AddComponent<LaunchedEnemy>();
            enemyToLaunch[i].AddComponent<Rigidbody>();
            launchedEnemy = enemyToLaunch[i].GetComponent<LaunchedEnemy>();
            launchedEnemy.startingPosition = enemyStartingPosition[i].transform.position;
            enemyToLaunch[i].transform.rotation = enemyStartingPosition[i].transform.rotation;            
            launchedEnemy.StartCoroutine(launchedEnemy.EnemyAssemblePosition());
            
            
            
        }
        
        

    }

    public void WaitForEnemiesInPositions() // here we... well it's kind'a self explanatory
    {

        
        int enemiesInPosition = 0;
        for (int i = 0; i < enemyToLaunch.Length; i++)
        {            
            launchedEnemy = enemyToLaunch[i].GetComponent<LaunchedEnemy>();
            if (launchedEnemy.enemyInStartingPosition)
            {
                enemiesInPosition++;
            } 
        }

        if (enemiesInPosition == enemyToLaunch.Length)
        {
            launchedEnemy = enemyToLaunch[11].GetComponent<LaunchedEnemy>();
            
            StartAttack();
        }

        
        
    }

    

    void StartAttack() //here we pass the info, so the enemyobjects will know what type of attack they should perform
    {
        
        if (startWaveAttack)
        {
            for (int i = 0; i < enemyToLaunch.Length; i++)
            {
                
                launchedEnemy = enemyToLaunch[i].GetComponent<LaunchedEnemy>();
                launchedEnemy.enemyWaveAttackSpeed = enemyWaveAttackSpeed;
                launchedEnemy.enemyWaveAttack = true;
                launchedEnemy.LaunchEnemy();
            }
        }

        if (startTopAttack)
        {
            for (int i = 0; i < enemyToLaunch.Length; i++)
            {
                
                launchedEnemy = enemyToLaunch[i].GetComponent<LaunchedEnemy>();
                launchedEnemy.enemyTopAttackSpeed = enemyTopAttackSpeed;
                launchedEnemy.enemyTopAttackAscendSpeed = enemyTopAttackAscendSpeed;
                launchedEnemy.enemyTopAttack = true;
                launchedEnemy.topAttackPosition = enemyTopAttackStartPos[i].transform.position;
                if (i > 5)
                {
                    launchedEnemy.secondWave = true;
                }
                launchedEnemy.LaunchEnemy();
            }
        }

        if (startColumnAttack) //here we form 2 columns and send them. Could do with only 1 loop. But eventually ended with 2 loops. RTFM
        {
            int column = Random.Range(minZeroNumber, enemyToLaunch.Length);
            Vector3 columnPos = enemyStartingPosition[column].transform.position;
            for (int i = 0; i < 6; i++)
            {
                
                launchedEnemy = enemyToLaunch[i].GetComponent<LaunchedEnemy>();
                launchedEnemy.enemyColumnAttackSpeed = enemyColumnAttackSpeed;
                launchedEnemy.enemyColumnAttack = true;
                float columnPosY = columnPos.y + i;
                columnPos = new Vector3(columnPos.x, columnPosY, columnPos.z);                
                launchedEnemy.columnAttackPos = columnPos;
                columnPos.y = 0;
                launchedEnemy.LaunchEnemy();
                
            }
            int column2 = Random.Range(minZeroNumber, enemyToLaunch.Length);
            while (column2 == column)
            {
                column2 = Random.Range(minZeroNumber, enemyToLaunch.Length);
            }
            Vector3 columnPos2 = enemyStartingPosition[column2].transform.position;
           for (int i = 6; i < enemyToLaunch.Length; i++)
            {

                launchedEnemy = enemyToLaunch[i].GetComponent<LaunchedEnemy>();
                launchedEnemy.enemyColumnAttackSpeed = enemyColumnAttackSpeed;
                launchedEnemy.enemyColumnAttack = true;
                float columnPosY = columnPos2.y + (i - 6);
                columnPos2 = new Vector3(columnPos2.x, columnPosY, columnPos2.z);
                launchedEnemy.columnAttackPos = columnPos2;
                columnPos2.y = 0;
                launchedEnemy.LaunchEnemy();

            }
        }

        if (startBurstAttack)
        {
            int burstPos = Random.Range(minZeroNumber, enemyToLaunch.Length);
            for (int i = 0; i < enemyToLaunch.Length; i++)
            {
                launchedEnemy = enemyToLaunch[i].GetComponent<LaunchedEnemy>();
                launchedEnemy.enemyBurstAttackSpeed = enemyBurstAttackSpeed;
                launchedEnemy.enemyBurstAttack = true;
                launchedEnemy.enemyBurstAttackDelayBeforeLaunch = minBurstDelay * i;
                launchedEnemy.enemyBurstAttackStartPos = enemyStartingPosition[burstPos].transform.position;
                launchedEnemy.LaunchEnemy();
            }
        }

        
        StartCoroutine(DelayBeetweenSubAttacks());
    }

    IEnumerator DelayBeetweenSubAttacks()
    {
        yield return new WaitForSeconds(delayBetweenSubWaves);
        WaveLauncherTracker();
    }

     
    //end of sending his part as enemies.
    #endregion
    public void BossIsAngry() //makes Boss change color. Like he is angry. Messes up with StartNewAttack method. Rewrite it, or do not use them simultaneously at least.
    {
        Debug.Log("BossIsAngry");
        GameObject[] glowers;
        glowers = GameObject.FindGameObjectsWithTag("Enemy");
        LaunchedEnemy enemyToGlow;
        StartCoroutine(PlayAngrySound());
        for (int i = 0; i < glowers.Length; i++)
        {
            glowers[i].AddComponent<LaunchedEnemy>();
            enemyToGlow = glowers[i].GetComponent<LaunchedEnemy>();
            enemyToGlow.angryOnly = true;
            StartCoroutine(enemyToGlow.GlowEveryone());
        }
    }

    IEnumerator PlayAngrySound()
    {
        
        bossSound.PlayOneShot(bossAngrySound, soundVolumeFull * 2);
        yield return new WaitForSeconds(1);
        bossSound.PlayOneShot(bossAngrySound, soundVolumeFull * 3);
        yield return new WaitForSeconds(1);
        bossSound.PlayOneShot(bossAngrySound, soundVolumeFull * 3);
        yield return new WaitForSeconds(1);


    }

   public void BossIsDead()
    {
        Debug.Log("Boss is DEAD");
        StartCoroutine(Death());
        bossSound.PlayOneShot(bossDeathSound, soundVolumeFull * 2);
    }

    IEnumerator Death()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].AddComponent<LaunchedEnemy>();
            enemies[i].AddComponent<Rigidbody>();
            launchedEnemy = enemies[i].GetComponent<LaunchedEnemy>();
            launchedEnemy.bossIsDying = true;
            launchedEnemy.angryOnly = true;
        }

        
        StartCoroutine(RemoveBossFromScreen());
        yield return null;
         
    }

    IEnumerator RemoveBossFromScreen()
    {
        yield return new WaitForSeconds(10);
        gameManager.WonTheGame();
        for (int i = 0; i < enemies.Length; i++)
        {
            Rigidbody RB = enemies[i].GetComponent<Rigidbody>();
            Destroy(RB);
        }
        bool moveOffScreen = true;
        while (moveOffScreen)
        {
            transform.Translate(Vector3.down * 4 * Time.deltaTime);
            if (transform.position.y >= 30)
            {
                moveOffScreen = false;
            }
            yield return null;
        }
        Destroy(gameObject);
    }











}
