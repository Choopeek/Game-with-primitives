using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Level2GameManager : MonoBehaviour
{
    [SerializeField] GameObject bossGO;
    public BossScript bossSCR;
    [SerializeField] GameObject fakeBoss;
    float fakeBossInPosBoundY = -9.2f;
    float fakeBossDownSpeed = 10f;
    
    [SerializeField] GameObject bossDropship;
    float bossDropshipBoundZ = 140f;
    float bossDropshipDropPositionBound = 115f;
    float bossDropshipArriveSpeed = 0.5f;

    bool gameIsActive;
    public bool playerIsMovingForward; 
    [SerializeField] GameObject groundParticleLeft;
    [SerializeField] GameObject groundParticleCenter;
    [SerializeField] GameObject groundParticleRight;


    void Start()
    {
       
        //bossGO.gameObject.SetActive(false);
        
        
        
    }

    
    void Update()
    {
        
    }

    IEnumerator MoveDropship()
    {
        bool dropshipArrives = true;
        float arriveSpeed = bossDropshipArriveSpeed;
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
        
    }

    IEnumerator DropshipAway()
    {
       
        bool DropshipLeaving = true;
        float leavingSpeed = 50f;
        float destroyBound = -130f;
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

    void AttackManager()
    {

    }
}
