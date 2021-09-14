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


    private float delayBetweenAttacks = 3f;

    private bool startWaveAttack;

    private bool startTopAttack;
    [SerializeField] GameObject[] enemyTopAttackStartPos;

    private bool startColumnAttack;

    private bool startBurstAttack;
    private int minZeroNumber = 0;    
    private float minBurstDelay = 0.1f;

    private Vector3[] initialBossUnitsPosition;
    private Vector3 bossDropDownPosition = new Vector3(-2.69f, 0, 50.66f);

    //values for difficulty changer. Gonna implement him later.
    public float enemyWaveAttackSpeed = 10f;
    public float enemyTopAttackAscendSpeed = 20f;
    public float enemyTopAttackSpeed = 10f;
    public float enemyColumnAttackSpeed = 10f;
    public float enemyBurstAttackSpeed = 30f;

    //values for the AttackManager
    int minAttackTypeValue = 1;
    int maxAttackTypeValue = 5; //5 is used cause the INT RandomRange do not pick the max value;
    int burstAttackNumber = 1;
    int columnAttackNumber = 2;
    int topAttackNumber = 3;
    int waveAttackNumber = 4;
    int rangeNumberOfAttacksMin = 1;
    int rangeNubmerOfAttacksMax = 10;
    int rangeNumberOfAttacksForOne = 8;
    int rangeNOAforMoreAttacks = 2;
    int rangeNOAforMoreAttacksMax = 5;






    void Start()
    {
        gameManager = GameObject.Find("Level2GameManager").GetComponent<Level2GameManager>();
        

        
    }

    public void BossAssemble()
    {
        
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        initialBossUnitsPosition = new Vector3[enemies.Length];
        for (int i = 0; i < enemies.Length; i++)
        {
            initialBossUnitsPosition[i] = enemies[i].transform.position;
            enemies[i].transform.position = bossDropDownPosition;
        }
        StartCoroutine(BossAssembleStage1());
        
    }

    IEnumerator BossAssembleStage1()
    {
        yield return new WaitForSeconds(1);
        bool assembled = false;
        while(!assembled)
        {
            int allAssembled = 0;
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].transform.position = Vector3.MoveTowards(enemies[i].transform.position, initialBossUnitsPosition[i], 5f * Time.deltaTime);
                if (enemies[i].transform.position == initialBossUnitsPosition[i])
                {
                    allAssembled++;
                }
                if (allAssembled == enemies.Length)
                {
                    assembled = true;
                }
            }
            
            yield return null;

        }
        
        AttackManager();
    }

    public void AttackManager() //here we calculate what type of attack boss will use. And How many times he will use it.
    {
        Debug.Log("New Attack Type is chosen");
        int chooseAttack = Random.Range(minAttackTypeValue, maxAttackTypeValue);
        if (chooseAttack == burstAttackNumber)
        {
            startBurstAttack = true;
        }
        if (chooseAttack == columnAttackNumber)
        {
            startColumnAttack = true;
        }
        if (chooseAttack == topAttackNumber)
        {
            startTopAttack = true;
        }
        if (chooseAttack == waveAttackNumber)
        {
            startWaveAttack = true;
        }

        int numberOfAttacks = Random.Range(rangeNumberOfAttacksMin, rangeNubmerOfAttacksMax);
        if (numberOfAttacks < rangeNumberOfAttacksForOne)
        {
            numberOfAttacks = rangeNumberOfAttacksMin;
        }
        else
        {
            numberOfAttacks = Random.Range(rangeNOAforMoreAttacks, rangeNOAforMoreAttacksMax);
        }

        StartNewAttack(numberOfAttacks);
        
        
    }

    
    


    //this bunch of methods makes the Boss to send his parts as enemies
    private void StartNewAttack(int numberOfAttacks) //here we find all ENEMY tagged objects. Form an array out of them. Then we choose the enemy to be launched and untag it. So we won't choose him twice.
    {

        Debug.Log("StartNewAttack() " + numberOfAttacks);
        for (int i = 0; i < enemyToLaunch.Length; i++)
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            enemyToLaunch[i] = enemies[Random.Range(minZeroNumber, enemies.Length)];
            enemyToLaunch[i].tag = "EnemyWaitingToBeLaunched";
            //in this function we give our enemies the position and rotation before enabling them to move towards the player.
            enemyToLaunch[i].AddComponent<LaunchedEnemy>();
            launchedEnemy = enemyToLaunch[i].GetComponent<LaunchedEnemy>();
            launchedEnemy.startingPosition = enemyStartingPosition[i].transform.position;
            enemyToLaunch[i].transform.rotation = enemyStartingPosition[i].transform.rotation;            
            launchedEnemy.StartCoroutine(launchedEnemy.EnemyAssemblePosition());
            if (i == 11)
            {
                
                launchedEnemy.numberOfAttacks = numberOfAttacks;

            }
            if (enemies.Length < 500)
            {
                BossIsDead();
            }
            
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
            int numberOfAttacks = launchedEnemy.numberOfAttacks;
            Debug.Log("WaitForEnemiesInPosiitons() " + numberOfAttacks);
            StartAttack(numberOfAttacks);
        }

        
        
    }

    

    void StartAttack(int numberOfAttacks) //here we pass the info, so the enemyobjects will know what type of attack they should perform
    {
        Debug.Log("StartAttack() " + numberOfAttacks);
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

        numberOfAttacks--;
        Debug.Log("attacks left after an attack " + numberOfAttacks);
        StartCoroutine(WaitForTheNextWaveToBeLaunched(numberOfAttacks));
    }

    IEnumerator WaitForTheNextWaveToBeLaunched(int numberOfAttacks) //here we (and why the fuck do I write "we"? I write this code alone) make the boss to wait until the current wave is all launched. And only after this - he start to launch the new wave
    {
        Debug.Log("WaitForTheNextWaveToBeLaunched() " + numberOfAttacks);
        yield return new WaitForSeconds(delayBetweenAttacks);
        if (numberOfAttacks > 0)
        {
            
            StartNewAttack(numberOfAttacks);
        }
        else //if no attacks left - boss resets the values for choosing attacks and goes one more time.
        {
            
            startBurstAttack = false;
            startColumnAttack = false;
            startTopAttack = false;
            startWaveAttack = false;
            AttackManager();
        }
        
    }
    //end of sending his part as enemies.

    private void BossIsAngry() //makes Boss change color. Like he is angry. Messes up with StartNewAttack method. Rewrite it, or do not use them simultaneously at least.
    {
        Debug.Log("BossIsAngry");
        GameObject[] glowers;
        glowers = GameObject.FindGameObjectsWithTag("Enemy");
        LaunchedEnemy enemyToGlow;
        for (int i = 0; i < glowers.Length; i++)
        {
            glowers[i].AddComponent<LaunchedEnemy>();
            enemyToGlow = glowers[i].GetComponent<LaunchedEnemy>();
            StartCoroutine(enemyToGlow.GlowEveryone());
        }
    }

    private void BossIsDead()
    {
        Debug.Log("Boss is DEAD");
        Destroy(gameObject);
    }











}
