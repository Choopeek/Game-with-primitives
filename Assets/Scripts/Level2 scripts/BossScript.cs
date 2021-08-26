using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    private LaunchedEnemy script;
    public GameObject[] enemies;
    public GameObject[] enemyStartingPosition;
    public GameObject[] enemyToLaunch = new GameObject[12];
    
    // Start is called before the first frame update
    void Start()
    {
                
        StartWave();

    }

    
    void Update()
    {
        
    }

    private void StartWave() //here we find all ENEMY tagged objects. Form an array out of them. Then we choose the enemy to be launched and untag it. So we won't choose him twice.
    {
        for (int i = 0; i < enemyToLaunch.Length; i++)
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            enemyToLaunch[i] = enemies[Random.Range(0, enemies.Length)];
            enemyToLaunch[i].tag = "Untagged";
        }

        MoveEnemiesToPosition();


    }

    

    private void MoveEnemiesToPosition() //here I made it work the stupid way. Later I have to write a proper function. Maybe. 24.08.2021   UPD: made it with for loops 26.08.2021
    {
        //in this function we give our enemies the position and rotation before enabling them to move towards the player. Later I have to make them form smoothly.
        for (int i=0; i<enemyToLaunch.Length; i++)
        {
            enemyToLaunch[i].transform.position = enemyStartingPosition[i].transform.position;
            enemyToLaunch[i].transform.rotation = enemyStartingPosition[0].transform.rotation;
        }
            

        StartCoroutine(LaunchEnemyWave());
    }

    IEnumerator LaunchEnemyWave()
    {
        for (int i=0; i<enemyToLaunch.Length; i++)
        {
            enemyToLaunch[i].tag = "LaunchedEnemy";
            enemyToLaunch[i].AddComponent<LaunchedEnemy>();
        }
                           
        yield return new WaitForSeconds(0.1f); //yet I have to make them launched not simultaneously
        StartWave();
    }

    

    


}
