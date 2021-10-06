using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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


    bool gameIsActive;



    public bool playerIsMovingForward; 
    [SerializeField] GameObject groundParticleLeft;
    [SerializeField] GameObject groundParticleCenter;
    [SerializeField] GameObject groundParticleRight;

    private MusicController musicController;

    CameraAncor mainCameraController;

    //DIALOGUES
    [SerializeField] DialogueTrigger paskaDialogueObj;


    //boss AttackStuff
    int waveNumber = 0;
    
 
    #endregion

    void Start()
    {
        mainCameraController = GameObject.Find("Main Camera").GetComponent<CameraAncor>();
        musicController = GameObject.Find("Music").GetComponent<MusicController>();
        bossGO.gameObject.SetActive(false);
        gameIsActive = false;
        TurnParticlesONOFF(true);                
        dropshipSounds = GameObject.Find("BOSSTransport").GetComponent<BossDropShipSoundController>();
        //StartCoroutine(MoveDropship());
        musicController.MusicStop();
        HandleMainCamera();

        paskaDialogueObj.TriggerDialogue();
        
        
        
    }

    
    void Update()
    {
        
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
                bossSCR.BossAssemble();
                
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

    public void bossIsAssembled()
    {
        Debug.Log("boss is assembled and ready to fight");
    }


    public void WaveTracker()
    {
        waveNumber = bossSCR.waveNumber;
        Debug.Log("GameManager's " + "WaveNumber is " + waveNumber);
        bossSCR.WaveLauncherTracker();
        //place some IF's what happens when WaveNumber reaches some point.
    }
    

    void TurnParticlesONOFF(bool _TurnOFF) //if you pass the @FALSE@ it will turn on the particles
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
            mainCameraController.MoveCameraForStart();
            
        }
        
    }

}
