using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject enemyShipPrefab;

    //variables to track enemy waves
    public int enemiesCount;
    private int enemyWaveNumber;

    //variables to spawn enemy dropship
    private float spaceShipSpawnPosX = 18.80f;
    private float spaceShipSpawnPosYBound1 = 10.0f;
    private float spaceShipSpawnPosYBound2 = 23f;
    private float spaceShipSpawnPosZ = -30.27f;

    //variables to spawn enemy
    private float enemySpawnPosX1 = 15f;
    private float enemySpawnPosX2 = -15f;
    private float enemySpawnPosY1 = 13f;
    private float enemySpawnPosY2 = -8f;
    private float enemySpawnPosZ1 = -4.0f;
    private float enemySpawnPosZ2 = -12f;
    private float nearBaseSpawnLimiter = 2;

    //enemyWave spawn sound
    public AudioClip waveSpawnSound;
    private AudioSource spawnAudio;
    private float soundVolumeHalf = 0.3f;

    //implementing Game Over mechanics
    public bool gameIsActive;

    //implementing Flying Armada
    public List<GameObject> armadaShips;
    public GameObject armadaShipPrefab;
    private float armadaSpawnDelayMin = 2f;
    private float armadaSpawnDelayMax = 5f;

    private GameManager gameManager;

    public int waveNumberToChangeMusic = 10;
    public int waveToWinNumber = 5;

    
    // Start is called before the first frame update
    void Start()
    {
        spawnAudio = GetComponent<AudioSource>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
       
    }

    // Update is called once per frame
    void Update()
    {
        //track if we need to spawn enemies and increase the wave number
        enemiesCount = FindObjectsOfType<Enemy>().Length;
        if (enemiesCount == 0 && gameIsActive)
        {
            enemyWaveNumber++;
            SpawnEnemyWave(enemyWaveNumber);
            
        }
    }

    

    
    //spawns enemy wave and a spaceship
    public void SpawnEnemyWave(int enemiesToSpawn)
    {
        Instantiate(enemyShipPrefab, GeneraTeSpaceShipPos(), enemyShipPrefab.transform.rotation);
        spawnAudio.PlayOneShot(waveSpawnSound, soundVolumeHalf);
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateEnemyPosition(), enemyPrefab.transform.rotation);
        }   
        if(enemyWaveNumber == waveNumberToChangeMusic)
        {
            gameManager.MusicIntensifies();
            ArmadaIntensifies();
        }
        if(enemyWaveNumber >= waveToWinNumber)
        {
            StartCoroutine(Victory());
        }
    }
    //generate enemyship position
   private Vector3 GeneraTeSpaceShipPos()
    {
        float spawnPosY = Random.Range(spaceShipSpawnPosYBound1, spaceShipSpawnPosYBound2);
        Vector3 randomPos = new Vector3(spaceShipSpawnPosX, spawnPosY, spaceShipSpawnPosZ);
        return randomPos;
    }
    //generate enemy position
    private Vector3 GenerateEnemyPosition()
    {
        float spawnPosX = Random.Range(enemySpawnPosX1, enemySpawnPosX2); 
        while(spawnPosX < nearBaseSpawnLimiter && spawnPosX > -nearBaseSpawnLimiter) //these loops do not allow enemies to spawn too near to the base
        {
            spawnPosX = Random.Range(enemySpawnPosX1, enemySpawnPosX2);
        }
        float spawnPosY = Random.Range(enemySpawnPosY1, enemySpawnPosY2);
        while(spawnPosY < nearBaseSpawnLimiter && spawnPosY > -nearBaseSpawnLimiter)
        {
            spawnPosY = Random.Range(enemySpawnPosY1, enemySpawnPosY2);
        }
        float spawnPosZ = Random.Range(enemySpawnPosZ1, enemySpawnPosZ2);
        Vector3 enemyRandomPos = new Vector3(spawnPosX, spawnPosY, spawnPosZ);
        return enemyRandomPos;
    }
    //here we set the delay between spawning ArmadaShips
    public float ArmadaSpawnDelay()
    {
        float armadaSpawnDelay = Random.Range(armadaSpawnDelayMin, armadaSpawnDelayMax);
        return armadaSpawnDelay;
    }

    //here we generate the spawnposition of a spaceship. We do use the string for that
    public IEnumerator SpaceshipsArmadaFlying()
    {
        while(gameIsActive)
        {
            //Debug.Log("ArmadaActive"); //dont need it no more. everything works just fine now.
            yield return new WaitForSeconds(ArmadaSpawnDelay());
            int index = Random.Range(1, armadaShips.Count);
            Instantiate(armadaShips[index], GeneraTeSpaceShipPos(), armadaShipPrefab.transform.rotation);
            yield return null;
        }
    }

    IEnumerator Victory()
    {
        yield return new WaitForSeconds(1);
        gameManager.Victory();

    }

    private void ArmadaIntensifies()
    {
        armadaSpawnDelayMin = 0.2f;
        armadaSpawnDelayMax = 1.5f;
    }
    
}


