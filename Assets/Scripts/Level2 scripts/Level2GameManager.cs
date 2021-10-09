using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level2GameManager : MonoBehaviour
{
    #region Variables
    [SerializeField] GameObject bossGO;
    public BossScript bossSCR;
    [SerializeField] GameObject fakeBoss;
    float fakeBossInPosBoundY = -9.2f;
    float fakeBossDownSpeed = 10f;
    
    [SerializeField] GameObject bossDropship;
    float bossDropshipBoundZ = 140f;
    float bossDropshipDropPositionBound = 115f;
    float bossDropshipArriveSpeed = 0.5f;
    BossDropShipSoundController dropshipSounds;


    bool playerIsDead;
    bool wonTheGame;
    [SerializeField] Level2PlayerController player;


     
    [SerializeField] GameObject groundParticleLeft;
    [SerializeField] GameObject groundParticleCenter;
    [SerializeField] GameObject groundParticleRight;

    private MusicController musicController;

    CameraAncor mainCameraController;

    //DIALOGUES
    [SerializeField] DialogueTrigger paskaDialogueObj;
    [SerializeField] DialogueTrigger bossAssembledCutscene;
    [SerializeField] DialogueTrigger NowWeAreTalking;


    //boss AttackStuff
    int waveNumber = 0;

    //level progression bool's
    bool cutsceneFirst;
    bool cutsceneSecond;
    bool cutsceneThird;
    [SerializeField] int waveNumberForRPG = 7;
    [SerializeField] int waveNumberToWinGame = 15;

    [SerializeField] Button restartButton;
    [SerializeField] GameObject victoryText;
    


    
 
    #endregion

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    

    void Start()
    {
        mainCameraController = GameObject.Find("Main Camera").GetComponent<CameraAncor>();
        musicController = GameObject.Find("Music").GetComponent<MusicController>();
        bossGO.gameObject.SetActive(false);        
        TurnParticlesOFF(true);                
        dropshipSounds = GameObject.Find("BOSSTransport").GetComponent<BossDropShipSoundController>();
        
        musicController.MusicStop();
        HandleMainCamera();

        paskaDialogueObj.TriggerDialogue();
        cutsceneFirst = true;

        




    }

    
    

    public void StartTheGame()
    {
        Debug.Log("StartTheGame");
        StartCoroutine(MoveDropship());
    }
    IEnumerator MoveDropship()
    {
        bool dropshipArrives = true;
        float arriveSpeed = bossDropshipArriveSpeed;
        dropshipSounds.PlayEngineSound();
        while (dropshipArrives)
        {
            bossDropship.transform.position = Vector3.MoveTowards(bossDropship.transform.position, new Vector3(bossDropship.transform.position.x, bossDropship.transform.position.y, bossDropshipDropPositionBound), arriveSpeed);
            
            if (bossDropship.transform.position.z < bossDropshipBoundZ)
            {
                arriveSpeed = 0.05f;
                
            }
            if (bossDropship.transform.position.z <= bossDropshipDropPositionBound)
            {
                dropshipArrives = false;
                StartCoroutine(DropTheBOSS());
                
            }
            yield return null;
        }
             
    }

    IEnumerator DropTheBOSS()
    {
        
        bool bossIsDropped = false;
        musicController.Lvl2MusicCalm();
        StartCoroutine(DropshipAway());
        while (!bossIsDropped)
        {
            fakeBoss.transform.Translate(Vector3.down * fakeBossDownSpeed * Time.deltaTime);
            if (fakeBoss.transform.position.y <= fakeBossInPosBoundY)
            {
                bossIsDropped = true;
                bossGO.gameObject.SetActive(true);
                Destroy(fakeBoss.gameObject);                
                bossSCR = GameObject.Find("BOSS").GetComponent<BossScript>();                
                StartCoroutine(bossSCR.BossAssemble());
                
            }
            
            yield return null;
        }

        
        yield return null;
        
        
    }

    
    IEnumerator DropshipAway()
    {
       
        bool DropshipLeaving = true;
        float leavingSpeed = 50f;
        float destroyBound = -650f;       
        while (DropshipLeaving)
        {
            bossDropship.transform.Translate(Vector3.forward * leavingSpeed * Time.deltaTime);
            if (bossDropship.transform.position.z <= destroyBound)
            {
                
                Destroy(bossDropship.gameObject);
                DropshipLeaving = false;
                
            }
            yield return null;
        }
    }


    //here we will manage a big load of stuff about boss

    public void bossIsAssembled() //
    {

        bossAssembledCutscene.TriggerDialogue();        
        
        
        
        
    }


    public void WaveTracker() //here we track the waves, and make them harder or easier;
    {
        waveNumber++;
        bossSCR.waveNumber = waveNumber;
        Debug.Log("GameManager's " + "WaveNumber is " + waveNumber);

        if (waveNumber == waveNumberToWinGame - 1)
        {
            bossSCR.BossIsAngry();
            StartCoroutine(WaitForAngry());
            return;
        }

        if (waveNumber == waveNumberToWinGame)
        {
            
            bossSCR.BossIsDead();

            return;
            //endGame
        }
        if (waveNumber == 4 | waveNumber == 6 | waveNumber == 10 )
        {
            MakeGameHarder();
            bossSCR.BossIsAngry();
            StartCoroutine(WaitForAngry());
            return;
        }   
                

        if (waveNumber == waveNumberForRPG)
        {
            StartCoroutine(WaitForEnemiesToClear());            
            return;
        }

        
        
        bossSCR.WaveLauncherTracker();
        //place some IF's what happens when WaveNumber reaches some point.
    }



    void MakeGameHarder()
    {
        
        float harderDelayModifier = 0.8f;
        float harderSpeedModifier = 1.2f;
        bossSCR.delayBetweenWaves = bossSCR.delayBetweenSubWaves * harderDelayModifier;
        bossSCR.delayBetweenSubWaves = bossSCR.delayBetweenSubWaves * harderDelayModifier;
        //bossSCR.enemyBurstAttackSpeed = bossSCR.enemyBurstAttackSpeed * harderSpeedModifier;
        bossSCR.enemyColumnAttackSpeed = bossSCR.enemyColumnAttackSpeed * harderSpeedModifier;
        bossSCR.enemyTopAttackAscendSpeed = bossSCR.enemyTopAttackAscendSpeed * harderSpeedModifier;
        //bossSCR.enemyTopAttackSpeed = bossSCR.enemyTopAttackSpeed * harderSpeedModifier;
        bossSCR.enemyWaveAttackSpeed = bossSCR.enemyWaveAttackSpeed * harderSpeedModifier;
    }

    void MakeGameEasier()
    {
        Debug.Log("MakingGame easier");
        float easierDelayModifier = 1.1f;
        float easierSpeedModifier = 0.9f;
        bossSCR.delayBetweenWaves = bossSCR.delayBetweenSubWaves * easierDelayModifier;
        bossSCR.delayBetweenSubWaves = bossSCR.delayBetweenSubWaves * easierDelayModifier;
        bossSCR.enemyBurstAttackSpeed = bossSCR.enemyBurstAttackSpeed * easierSpeedModifier;
        bossSCR.enemyColumnAttackSpeed = bossSCR.enemyColumnAttackSpeed * easierSpeedModifier;
        bossSCR.enemyTopAttackAscendSpeed = bossSCR.enemyTopAttackAscendSpeed * easierSpeedModifier;
        bossSCR.enemyTopAttackSpeed = bossSCR.enemyTopAttackSpeed * easierSpeedModifier;
        bossSCR.enemyWaveAttackSpeed = bossSCR.enemyWaveAttackSpeed * easierSpeedModifier;
    } //unused. Maybe it'll be a good idea to make game easier every time player loses it.
    

    void TurnParticlesOFF(bool _TurnOFF) //if you pass the @FALSE@ it will turn on the particles
    {
        bool turnOFF = _TurnOFF;
        if (turnOFF)
        {
            groundParticleCenter.gameObject.SetActive(false); 
            groundParticleLeft.gameObject.SetActive(false);
            groundParticleRight.gameObject.SetActive(false);
        }

        else
        {
            groundParticleCenter.gameObject.SetActive(true);
            groundParticleLeft.gameObject.SetActive(true);
            groundParticleRight.gameObject.SetActive(true);
        }
        
    }

    //make Cameramove in position

    void HandleMainCamera()
    {
        mainCameraController.PrepareForStart();
    }

    public void StartExitDialogueState(string dialogueState)
    {
        if (dialogueState == "start")
        {
            
            //here you define what happens in game when you enter the DialogueStates (yep, gonna learn the state machine one day)
        }
        if (dialogueState == "exit")
        {
            LevelProgression();
            
        }
        
    }

    void LevelProgression()
    {
        if (cutsceneFirst)
        {
            mainCameraController.MoveCameraForStart();
            cutsceneFirst = false;
            cutsceneSecond = true;
            return;
        }

        if (cutsceneSecond)
        {
            cutsceneSecond = false;
            cutsceneThird = true;
            WaveTracker();            
            return;
        }

        if (cutsceneThird)
        {
            cutsceneThird = false;            
            TurnParticlesOFF(false);
            WaveTracker();
            return;
        }
    }

    IEnumerator WaitForEnemiesToClear()
    {
        yield return new WaitForSeconds(3);
        
        if (waveNumber == waveNumberForRPG)
        {
            StartCoroutine(player.EnlargeYourRPG());
            yield return new WaitForSeconds(2);
            musicController.Lvl2MusicDynamic();
            NowWeAreTalking.TriggerDialogue();
        }
                

    }

    IEnumerator WaitForAngry()
    {
        yield return new WaitForSeconds(5);
        WaveTracker();
    }

    public void PlayerIsDead()
    {
        Debug.Log("player is dead");
        StartCoroutine(ShowRestartButton());
        player.Death();
    }

    public void WonTheGame()
    {
        Debug.Log("VICTORY");
        victoryText.gameObject.SetActive(true);
    }

    IEnumerator ShowRestartButton()
    {
        yield return new WaitForSeconds(2);
        restartButton.gameObject.SetActive(true);
        
    }
}
